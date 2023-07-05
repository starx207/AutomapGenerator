namespace AutomapGenerator.FunctionalTests.Models;
public class DocMetaData : AuditEntity {
    public Guid Id { get; set; }
    public string? DocTitle { get; set; }
    public string? DocUser { get; set; }
    public DateTime? DocDate { get; set; }
    public DateTime? TimeStamp { get; set; }
    public int? DocDateTypeIdx { get; set; }
    public DateTime? DeleteDate { get; set; }
    public long? Bcs { get; set; }
    public long? DocLen { get; set; }
    public int? InstanceNameId { get; set; }
    public DateTime? DateTimeAddedToMaster { get; set; }
    public DocType? Type { get; set; }
    public Document DocContent { get; set; } = new();
    public ICollection<Keyword> Keywords { get; set; } = new HashSet<Keyword>();
}
