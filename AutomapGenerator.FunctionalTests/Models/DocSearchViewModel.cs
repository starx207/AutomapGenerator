namespace AutomapGenerator.FunctionalTests.Models;
public class DocSearchViewModel {
    public Guid Id { get; set; }
    public string? DocTitle { get; set; }
    public string? Creator { get; set; }
    public DateTime? CreateDate { get; set; }
    public DateTime? ChangeDate { get; set; }
    public DateTime? DocDeleteDate { get; set; }
    public DateTime? DocDate { get; set; }
    public long? Length { get; set; }
    public string? Type { get; set; }
    public int? SortIndex { get; set; }
}
