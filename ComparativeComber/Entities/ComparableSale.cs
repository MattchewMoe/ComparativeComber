using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace ComparativeComber.Entities;

public class ComparableSale
{
    [Key]
    public int ComparableSaleId { get; set; }

    // From ComparableSale
    public string? OriginatingReportUrl { get; set; }
    public string? Location { get; set; }
    public string? ParcelNumbers { get; set; }
    public string? SchoolDistrict { get; set; }
    public string? Grantor { get; set; }
    public string? Grantee { get; set; }
    public string? PropertyData { get; set; }
    public string? Type { get; set; }
    public string? ConstructionType { get; set; }

    public string? UseCode { get; set; }

    public bool? IsVerified { get; set; }

    public bool? FromInternalReport { get; set; }

    public bool? FromHainesReport { get; set; }

 
    public string? SalePriceString { get; set; }
    public string? Condition { get; set; }
    public string? DateOfSaleString { get; set; }
    public string? RecordingReference { get; set; }
    public string? PropertyRightsTransferred { get; set; }
    public string? CircumstancesOfSale { get; set; }
    public string? Financing { get; set; }
    public string? PresentUse { get; set; }
    public string? HighestAndBestUse { get; set; }
    public string? Stories { get; set; }
    public string? YearConstructedString { get; set; }
    public string? SizeInSF { get; set; }
    public string? SiteSize { get; set; }
    public string? BuildingSize { get; set; }
    public string? ImprovementSize { get; set; }
    public string? NumberOfUnits { get; set; }
    public string? RentData { get; set; }
    public string? IntendedUse { get; set; }
    public string? LandToBuildingRatio { get; set; }
    public string? TrafficCount { get; set; }
    public string? Parking { get; set; }
    public string? ParkingRatio { get; set; }
    public string? SiteSizeInAcreage { get; set; }
    public string? Indication { get; set; }
    public string? Configuration { get; set; }
    public string? Topography { get; set; }
    public string? Utilities { get; set; }
    public string? Zoning { get; set; }
    public string? FloodPlain { get; set; }
    public string? VerificationName { get; set; }
    public string? VerifiersRelationshipToSale { get; set; }
    public string? DateVerifiedString { get; set; }
    public string? PersonWhoVerifiedSale { get; set; }
    public string? TelephoneNumber { get; set; }
    public string? Comments { get; set; }
    public List<string>? ImageUrls { get; set; }

    //Fields for Heins reports
    public string? HainesReportSaleNumber { get; set; }
   
    public string? TaxingDistrict { get; set; }
    public string? GrantorPrincipal { get; set; }
    public string? GranteePrincipal { get; set; }
    public string? GranteeAddress { get; set; }
    public string? GranteePhone { get; set; }
    public string? ORBookPage { get; set; }

    
    public decimal? FinancingAmount { get; set; }
    public decimal? AssessedValueLand { get; set; }
    public decimal? AssessedValueImp { get; set; }
    public decimal? AssessedValueTotal { get; set; }
    public decimal? AnnualTaxes { get; set; }

    // Typed properties from ComparableSaleSearchable
    public string? BuildingSizeUnit { get; set; }
    public double? BuildingSizeValue { get; set; }
    public decimal? SalePrice { get; set; }
    public DateTime? DateOfSale { get; set; }
    public int? YearConstructed { get; set; }
    public decimal? IndicationValue { get; set; }
    public string? IndicationUnitType { get; set; }
    public double? SiteSizeValue { get; set; }
    public string? SiteSizeUnit { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
    public string? City { get; set; }
    public string? County { get; set; }
    public string? StreetAddress { get; set; }
    public string? State { get; set; }
    public string? PostalCode { get; set; }
    public List<int>? RenovationYears { get; set; }

    // Additional properties if needed
}
