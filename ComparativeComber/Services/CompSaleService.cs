
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using System.Reflection;
using System.Text.RegularExpressions;
using ComparativeComber.Entities;
using ComparativeComber.Data;
using ComparativeComber.GeoServices;
using AutoMapper;
using ClosedXML.Excel;
using F23.StringSimilarity;




namespace ComparativeComber.Services
{
    public interface ICompSaleService
    {

        public Task<(IEnumerable<ComparableSaleDto> Data, int TotalCount)> SearchAndFetchComparableSalesAsync(
         decimal? salePriceMin = null,
        decimal? salePriceMax = null,
         double? buildingSizeMin = null,
        double? buildingSizeMax = null,
        double? siteSizeMin = null,
        double? siteSizeMax = null,
        DateTime? dateOfSaleStart = null,
        DateTime? dateOfSaleEnd = null,
        string? zoning = null,
        double? longitude = null,
        double? latitude = null,
        double? distanceMiles = null,
        List<string> cities = null, // Changed to List<string>
        List<string> counties = null, // Changed to List<string>
        string? postalCode = null,
        int page = 1,
        int pageSize = 10,
        CancellationToken cancellationToken = default);

        Task<ComparableSaleDto> AddCompSaleAsync(ComparableSale compSale, CancellationToken cancellationToken = default);
        Task UpdateCompSaleAsync(ComparableSale compSale, CancellationToken cancellationToken = default);
        Task<ImportResult<ComparableSaleDto>> ImportCompSalesFromExcelAsync(Stream excelStream, CancellationToken cancellationToken = default);






    }

    public class CompSaleService : ICompSaleService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly GeoLocationService _geoLocationService;

        public CompSaleService(
            AppDbContext context,
            IMapper mapper,
            GeoLocationService geoLocationService)  // Inject GeoLocationService here
        {
            _context = context;
            _mapper = mapper;
            _geoLocationService = geoLocationService;  // And store it
        }
        public async Task<(IEnumerable<ComparableSaleDto> Data, int TotalCount)> SearchAndFetchComparableSalesAsync(
           decimal? salePriceMin = null,
           decimal? salePriceMax = null,
           double? buildingSizeMin = null,
           double? buildingSizeMax = null,
           double? siteSizeMin = null,
           double? siteSizeMax = null,
           DateTime? dateOfSaleStart = null,
           DateTime? dateOfSaleEnd = null,
           string? zoning = null,
           double? longitude = null,
           double? latitude = null,
           double? distanceMiles = null,
           List<string> cities = null,
           List<string> counties = null,
           string? postalCode = null,
           int offset = 1,
           int pageSize = 10,
          
           CancellationToken cancellationToken = default)
        {
            IQueryable<ComparableSale> query;

            // Calculate the correct offset based on the page number
            int calculatedOffset = (offset - 1) * pageSize;

            if (longitude.HasValue && latitude.HasValue && distanceMiles.HasValue)
            {
                // Use the geospatial data
                var compSalesSubset = await _context.GetComparableSalesWithinDistanceAsync(longitude.Value, latitude.Value, distanceMiles.Value, cancellationToken);
                query = compSalesSubset.AsQueryable();
            }
            else
            {
                // Apply default pagination if no geospatial filter is provided
                query = _context.ComparableSales.AsNoTracking();
            }


            if (salePriceMin.HasValue)
                query = query.Where(cs => cs.SalePrice >= salePriceMin.Value);
            if (salePriceMax.HasValue)
                query = query.Where(cs => cs.SalePrice <= salePriceMax.Value);
            if (dateOfSaleStart.HasValue)
                query = query.Where(cs => cs.DateOfSale >= dateOfSaleStart.Value);
            if (dateOfSaleEnd.HasValue)
                query = query.Where(cs => cs.DateOfSale <= dateOfSaleEnd.Value);
            if (!string.IsNullOrEmpty(postalCode))
                query = query.Where(cs => cs.PostalCode.Contains(postalCode));
            if (!string.IsNullOrEmpty(zoning))
                query = query.Where(cs => cs.Zoning.Contains(zoning));

            // City and County filters
            if (cities != null && cities.Any())
                query = query.Where(cs => cities.Contains(cs.City));
            if (counties != null && counties.Any())
                query = query.Where(cs => counties.Contains(cs.County));



            var totalCount = await query.CountAsync();

            // Apply pagination

            query = query.Skip(offset).Take(pageSize);

            var filteredSales = await query.ToListAsync(cancellationToken);

            var data = _mapper.Map<IEnumerable<ComparableSaleDto>>(filteredSales);
            return (data, totalCount);
        }


        /// NOT USING THIS METHOD, MOVED THIS TO A STORED PROCEDURE IN THE DATABASE. Leaving it to maybe one day benchmark the two methods.
        private double DistanceBetweenPoints(double lat1, double lon1, double lat2, double lon2)
        {
            var R = 6371e3; // metres
            var φ1 = lat1 * Math.PI / 180; // φ, λ in radians
            var φ2 = lat2 * Math.PI / 180;
            var Δφ = (lat2 - lat1) * Math.PI / 180;
            var Δλ = (lon2 - lon1) * Math.PI / 180;

            var a = Math.Sin(Δφ / 2) * Math.Sin(Δφ / 2) +
                    Math.Cos(φ1) * Math.Cos(φ2) *
                    Math.Sin(Δλ / 2) * Math.Sin(Δλ / 2);
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            return R * c; // in metres
        }
        public async Task<ImportResult<ComparableSaleDto>> ImportCompSalesFromExcelAsync(Stream excelStream, CancellationToken cancellationToken = default)
        {
            var compSales = new List<ComparableSale>();
            var result = new ImportResult<ComparableSaleDto>();
            int totalRowsProcessed = 0;

            try
            {
                using (var workbook = new XLWorkbook(excelStream))
                {
                    foreach (var worksheet in workbook.Worksheets)
                    {
                        var headerRow = worksheet.FirstRowUsed();
                        AddMissingHeaders(headerRow);

                        var rows = worksheet.RowsUsed().Skip(1); // Skip header row
                        foreach (var row in rows)
                        {
                            var compSale = MapRowToComparableSale(row, headerRow);
                            compSales.Add(compSale);
                            totalRowsProcessed++;
                        }
                    }
                }

                await SaveAndPopulateResult(compSales, result, cancellationToken);
            }
            catch (Exception ex)
            {
                HandleImportError(result, ex, totalRowsProcessed);
            }

            return result;
        }

        private void AddMissingHeaders(IXLRow headerRow)
        {
            var lastColumn = headerRow.LastCellUsed().Address.ColumnNumber;

            // Check and add Latitude header if not present
            bool latitudeAdded = false;
            if (!headerRow.Cells().Any(c => c.Value.ToString().Equals("Latitude", StringComparison.OrdinalIgnoreCase)))
            {
                headerRow.Cell(lastColumn + 1).Value = "Latitude";
                lastColumn++;
                latitudeAdded = true;
            }

            // Check and add Longitude header if not present
            if (!headerRow.Cells().Any(c => c.Value.ToString().Equals("Longitude", StringComparison.OrdinalIgnoreCase)))
            {
                headerRow.Cell(lastColumn + (latitudeAdded ? 1 : 0)).Value = "Longitude";
            }
        }

        private ComparableSale MapRowToComparableSale(IXLRow row, IXLRow headerRow)
        {
            var compSale = new ComparableSale();

            for (int i = 1; i <= row.CellCount(); i++)
            {
                var cell = row.Cell(i);
                string header = (i <= headerRow.CellCount()) ? GetHeaderForCell(headerRow, i) : GetSpecialHeader(i, row.CellCount());
                SetProperty(compSale, header, cell.Value.ToString());
            }

            HandleSpecialCases(compSale, row);

            return compSale;
        }

        private string GetHeaderForCell(IXLRow headerRow, int columnIndex)
        {
            return headerRow.Cell(columnIndex).Value.ToString();
        }

        private string GetSpecialHeader(int columnIndex, int totalColumns)
        {
            if (columnIndex == totalColumns - 1)
                return "Latitude";
            else if (columnIndex == totalColumns)
                return "Longitude";
            return string.Empty;
        }
   
        private void HandleSpecialCases(ComparableSale compSale, IXLRow row)
        {
            // Custom logic for handling special cases
        }

        private async Task SaveAndPopulateResult(List<ComparableSale> compSales, ImportResult<ComparableSaleDto> result, CancellationToken cancellationToken)
        {
            try
            {
                await _context.ComparableSales.AddRangeAsync(compSales, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
                result.Data = _mapper.Map<List<ComparableSaleDto>>(compSales);
                result.TotalRecords = compSales.Count;
                result.ImportedRecords = compSales.Count;
                result.IsSuccessful = true;
                result.Message = "Data imported successfully.";
            }
            catch (Exception ex)
            {
                result.IsSuccessful = false;
                result.Message = $"An error occurred during saving or mapping: {ex.Message}";
            }
        }
      



        private void HandleImportError(ImportResult<ComparableSaleDto> result, Exception ex, int totalRowsProcessed)
        {
            // Logic to handle import error
            // ...
        }






        private Dictionary<string, string> manualMappings = new Dictionary<string, string>
{
    // Direct mappings
    { "SalePrice", "SalePrice" },
    { "SaleDate", "DateOfSale" },
    { "GranteeAddress", "GranteeAddress" },
    { "ORBook/Page", "ORBookPage" },
    { "ParcelNumber", "ParcelNumbers" },
    { "PropertyAddressState", "State" },
    { "Utilities", "Utilities" },
    { "AssessedValue-Imp", "AssessedValueImp" },
    { "AssessedValue-Total", "AssessedValueTotal" },
    { "Zoning", "Zoning" },
    { "GranteePrincipal", "GranteePrincipal" },
    { "SaleNo", "HainesReportSaleNumber" }, // Assuming SaleNo maps to HainesReportSaleNumber
    { "BuildingSize", "BuildingSize" },
    { "ConstructionType", "ConstructionType" },
    { "Comments", "Comments" },
    { "TaxingDistrict", "TaxingDistrict" },
    { "GranteePhone", "GranteePhone" },
    { "AnnualTaxes", "AnnualTaxes" },
    { "PropertyAddress(1)", "StreetAddress" }, // Assuming this maps to StreetAddress
    { "GrantorPrincipal", "GrantorPrincipal" },
    { "YearBuilt", "YearConstructedString" },
    { "PropertyZip", "PostalCode" },
    { "Grantor", "Grantor" },
    { "FinancingAmount", "FinancingAmount" },
    { "NoUnits", "NumberOfUnits" },
    { "PropertyZipCode", "PostalCode" },
    { "Grantee", "Grantee" },
    { "PropertyAddressCity", "City" },
    { "PropertyType", "Type" },
    { "SiteSize", "SiteSize" },
    { "AssessedValue-Land", "AssessedValueLand" },
    { "Financing", "Financing" },
            {"Longitude","Longitude" },
            { "Latitude", "Latitude" },
            {"Use", "UseCode" },
            {"UseCode", "UseCode" }

};

        private void SetProperty(ComparableSale compSale, string header, string value)
        {
            PropertyInfo property = null;
           

            // Perform manual mapping for all columns first
            if (manualMappings.TryGetValue(header, out string propertyName))
            {
                property = typeof(ComparableSale).GetProperty(propertyName);
                if (property != null && property.CanWrite)
                {
                    try
                    {
                        object convertedValue = ConvertValueToPropertyType(property, value);
                        if (convertedValue != null)
                        {
                            property.SetValue(compSale, convertedValue);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error setting property '{property.Name}' with value '{value}': {ex.Message}");
                    }
                }
            }

            
            if (property == null)
            {
                // Fallback to reflection for other unmapped columns
                property = FindClosestProperty(typeof(ComparableSale), header);
                //... existing reflection logic
            }
        }



        private object ConvertValueToPropertyType(PropertyInfo property, string value)
        {
            try
            {
                if (property.PropertyType == typeof(decimal?) || property.PropertyType == typeof(decimal))
                {
                    return decimal.TryParse(value, out var decimalResult) ? decimalResult : null;
                }
                if (property.PropertyType == typeof(DateTime?) || property.PropertyType == typeof(DateTime))
                {
                    return DateTime.TryParse(value, out var dateTimeResult) ? dateTimeResult : null;
                }
                if (property.PropertyType == typeof(double?) || property.PropertyType == typeof(double))
                {
                    return double.TryParse(value, out var doubleResult) ? doubleResult : null;
                }   
                // Add similar blocks for other special types

                // Default conversion for other types
                return Convert.ChangeType(value, property.PropertyType);
            }
            catch
            {
                // Log or handle the conversion error
                return null;
            }
        }

        private PropertyInfo FindClosestProperty(Type type, string key)
        {
            key = key.ToLower(); // Convert key to lowercase
            PropertyInfo closestProperty = null;
            double closestDistance = double.MaxValue;
          

            Levenshtein levenshtein = new Levenshtein(); // Assuming you have this class already defined

            foreach (var property in type.GetProperties())
            {
                double distance = levenshtein.Distance(key, property.Name.ToLower()); // Convert property name to lowercase

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestProperty = property;
                }

                if (closestDistance == 0)  // Early exit if perfect match
                {
                    return closestProperty;
                }

            }

            return  closestProperty;
        }

        public Task<ComparableSaleDto> AddCompSaleAsync(ComparableSale compSale, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task UpdateCompSaleAsync(ComparableSale compSale, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
        public class ImportResult<T>
        {
            public List<T>? Data { get; set; }
            public int TotalRecords { get; set; }
            public int ImportedRecords { get; set; }
            public bool IsSuccessful { get; set; }
            public string Message { get; set; }
        }
    

