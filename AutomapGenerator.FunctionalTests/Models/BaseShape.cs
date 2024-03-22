namespace AutomapGenerator.FunctionalTests.Models;
public abstract class BaseShape : AuditEntity {
    public Guid Id { get; set; }
    public Guid LayerId { get; set; }
    public string? ShapeColor { get; set; }
    public double? Altitude { get; set; }
    public string? Code { get; set; }
    public string? Description { get; set; }
    public bool Active { get; set; } = true;
    public string? AdditionalInfo { get; set; }
    public string? ExternalId { get; set; }
    public DateTime? DateLastMoved { get; set; }
    public bool BuildingNotInCamaData { get; set; } = true;
    public bool BuildingNotInCamaSketch { get; set; } = true;
    public DateTime? DateLastAreaChanged { get; set; }
    public bool Orphaned { get; set; } = false;
    public bool DeleteShapePending { get; set; } = false;
    public string? SourceRecId { get; set; }
    public int? BinaryCheckSum { get; set; }
    public double? ShapeArea { get; set; }
    public double? RotationAngle { get; set; }
    public int? Order { get; set; }
}

public abstract class BaseShape<TLabel> : BaseShape where TLabel : BaseLabel {
    public BaseShape() => TbWebLabels = new HashSet<TLabel>();

    public virtual ICollection<TLabel> TbWebLabels { get; set; }
}
