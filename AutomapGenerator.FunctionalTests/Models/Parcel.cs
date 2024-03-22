namespace AutomapGenerator.FunctionalTests.Models;
public class Parcel {
    public string Number { get; set; }
    public string? SourceRecKey { get; set; }
    public bool IsRetired { get; set; }
    public string? RetiredDate { get; set; }
    public string? RetiredReason { get; set; }
    public string? SitusParcelNbr { get; set; }
    public string? DeededName { get; set; }
    public string? District { get; set; }
    public string? ClassCode { get; set; }
    public string? LandUse { get; set; }
    public string? LandUseDescription { get; set; }
    public string? NeighborhoodCode { get; set; }
    public string? NeighborhoodDescription { get; set; }
    public decimal? TotalAcres { get; set; }
    public long? AppraisedLandValue { get; set; }
    public long? AppraisedImprovementValue { get; set; }
    public string? PropertyAddress { get; set; }
    public string? PropertyCity { get; set; }
    public string? PropertyZip { get; set; }
    public string? SchoolDistrict { get; set; }
    public string? LegalDescription { get; set; }
    public string? TownshipCity { get; set; }
    public string? Subdivision { get; set; }
    public string? SubdivisionDescription { get; set; }
    public string? ReviewCycle { get; set; }
    public string? GISCode { get; set; }
    public string? MapNumber { get; set; }

    public Parcel(string number) => Number = number;
}
