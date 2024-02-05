
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CompComber.Shared;

public class ComparableSaleDto
{

    public int? ComparableSaleId { get; set; }

    public string? County { get; set; }
    public string? City { get; set; }
    public string? PostalCode { get; set; }


    public string? OriginatingReportUrl { get; set; }
    public string? Location { get; set; }
    public string? ParcelNumbers { get; set; }
    public string? SchoolDistrict { get; set; }
    public string? Grantor { get; set; }
    public string? Grantee { get; set; }

    public string? Latitude { get; set; }
    public string? Longitude { get; set; }

    public string? PropertyData { get; set; }
    public string? Type { get; set; }

    public string? SalePrice { get; set; }

    public string? ConstructionType { get; set; }
    public string? Condition { get; set; }
    public string? DateOfSale { get; set; }
    public string? RecordingReference { get; set; }
    public string? PropertyRightsTransferred { get; set; }
    public string? CircumstancesOfSale { get; set; }
    public string? Financing { get; set; }
    public string? PresentUse { get; set; }
    public string? HighestAndBestUse { get; set; }

    public string? Stories { get; set; }
    public string? YearConstructed { get; set; }

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
    public string? DateVerified { get; set; }
    public string? PersonWhoVerifiedSale { get; set; }
    public string? TelephoneNumber { get; set; }
    public string? Comments { get; set; }
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
    public List<string>? ImageUrls { get; set; }
}
