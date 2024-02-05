
using ComparativeComber.Services;
using Microsoft.AspNetCore.Mvc;
using ComparativeComber.Entities;

namespace ComparativeComber
{
    [ApiController]
    [Route("[controller]")]
    public class CompSalesController : ControllerBase
    {
        private readonly ICompSaleService _compSaleService;
        private readonly ILogger<CompSalesController> _logger;

        public CompSalesController(ICompSaleService compSaleService, ILogger<CompSalesController> logger)
        {
            _compSaleService = compSaleService;
            _logger = logger;
        }
        [HttpGet]
        public async Task<IActionResult> SearchAndFetchComparableSales(
           
            decimal? salePriceMin = null,
            decimal? salePriceMax = null,
            int? buildingSizeMin = null,
            int? buildingSizeMax = null,
            int? siteSizeMin = null,
            int? siteSizeMax = null,
            DateTime? dateOfSaleStart = null,
            DateTime? dateOfSaleEnd = null,
            string? zoning = null,
            double? longitude = null,
            double? latitude = null,
            double? distanceMiles = null,
            [FromQuery] List<string>? cities = null, // Changed to a List
            [FromQuery] List<string>? counties = null, // Changed to a List
            string? postalCode = null,
            int page = 1,
            int pageSize = 10,
            bool? isHainesReport = false,
            bool? isInternalReport = false,
            bool? isVerified = false) // Default values)
        {
            // Ensure page and pageSize are valid
            page = Math.Max(page, 1); // Ensure page is at least 1
            pageSize = Math.Max(pageSize, 1); // Ensure pageSize is at least 1

            try
            {
                var offset = (page - 1) * pageSize; // Calculate the number of records to skip

                var result = await _compSaleService.SearchAndFetchComparableSalesAsync(
            salePriceMin, salePriceMax, buildingSizeMin, buildingSizeMax,
            siteSizeMin, siteSizeMax, dateOfSaleStart, dateOfSaleEnd, zoning,
            longitude, latitude, distanceMiles, cities, counties, postalCode, offset, pageSize);

                // Convert lists to string for logging
                var citiesString = cities != null ? String.Join(", ", cities) : "None";
                var countiesString = counties != null ? String.Join(", ", counties) : "None";

                
                var response = new
                {
                    Data = result.Data,
                    TotalCount = result.TotalCount,
                    CurrentPage = page,
                    PageSize = pageSize
                };
                _logger.LogInformation($"Sale Price Min: " + salePriceMin+
                    $"Sale Price Max: " + salePriceMax +
                    $"Building Size Min: " + buildingSizeMin +
                    $"Building Size Max: " + buildingSizeMax +
                    $"Site Size Min: " + siteSizeMin +
                    $"Site Size Max: " + siteSizeMax +
                    $"Date of Sale Start: " + dateOfSaleStart +
                    $"Date of Sale End: " + dateOfSaleEnd+
                    $"Zoning: " + zoning+
                    $"Longitude: " + longitude+
                    $"Latitude: " + latitude+
                    $"Distance Miles: " + distanceMiles+
                    $"Cities: " + citiesString+
                    $"Counties: " + countiesString+
                    $"Postal Code: " + postalCode +
                    $"Page: " + page +
                    $"Page Size: " + pageSize +
                    $"Is Haines Report: " + isHainesReport +
                    $"Is Internal Report: " + isInternalReport +
                    $"Is Verified: " + isVerified
                    );
                _logger.LogInformation($"Fetched {result.Data.Count()} comparable sales for page {page} with page size {pageSize}.");
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to fetch comparable sales: {ex}");
                return BadRequest("Failed to fetch comparable sales");
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateCompSale([FromBody] ComparableSale compSale)
        {
            try
            {
                await _compSaleService.AddCompSaleAsync(compSale);
                _logger.LogInformation("New comp sale added: {compSaleId}", compSale.ComparableSaleId);
                return CreatedAtAction(nameof(CreateCompSale), new { id = compSale.ComparableSaleId }, compSale);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to add new comp sale: {ex}");
                return BadRequest("Could not add comp sale");
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> EditCompSale(int id, [FromBody] ComparableSale compSale)
        {
            if (id != compSale.ComparableSaleId)
            {
                return BadRequest("ID mismatch, are you trying to pull a fast one?");
            }

            try
            {
                await _compSaleService.UpdateCompSaleAsync(compSale);
                _logger.LogInformation("Updated comp sale: {compSaleId}", compSale.ComparableSaleId);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to update comp sale: {ex}");
                return BadRequest("Could not update comp sale");
            }
        }

        [HttpPost("import")]
        public async Task<IActionResult> ImportCompSales(List<IFormFile> files)
        {
            if (files == null || !files.Any())
            {
                return BadRequest("No files provided or files are empty.");
            }

            var results = new List<FileImportResult<ComparableSaleDto>>();

            foreach (var file in files)
            {
                try
                {
                    using (var stream = file.OpenReadStream())
                    {
                        var importResult = await _compSaleService.ImportCompSalesFromExcelAsync(stream);
                        var fileResult = new FileImportResult<ComparableSaleDto>
                        {
                            FileName = file.FileName,
                            Data = importResult.Data,
                            TotalRecords = importResult.TotalRecords,
                            ImportedRecords = importResult.ImportedRecords,
                            IsSuccessful = importResult.IsSuccessful,
                            Message = importResult.Message
                        };

                        results.Add(fileResult);

                        if (importResult.IsSuccessful)
                        {
                            _logger.LogInformation($"Successfully imported {importResult.ImportedRecords} records out of {importResult.TotalRecords} from file {file.FileName}.");
                        }
                        else
                        {
                            _logger.LogError($"Error importing from file {file.FileName}: {importResult.Message}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error during file import from file {file.FileName}: {ex.Message}");
                    results.Add(new FileImportResult<ComparableSaleDto>
                    {
                        FileName = file.FileName,
                        IsSuccessful = false,
                        Message = $"Error during import: {ex.Message}"
                    });
                }
            }

            return Ok(results);
        }

    }
}
public class FileImportResult<T>
{
    public string FileName { get; set; }
    public List<T>? Data { get; set; }
    public int TotalRecords { get; set; }
    public int ImportedRecords { get; set; }
    public bool IsSuccessful { get; set; }
    public string Message { get; set; }
}
