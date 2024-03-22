namespace AutomapGenerator.FunctionalTests.Models;
public partial class CreateShapeCommand {
    public Guid LayerId { get; set; }
    public double? ShapeArea { get; set; }
    public string Code { get; set; }
    public string Description { get; set; }
    public double? Altitude { get; set; }
    public string? ShapeColor { get; set; }
    public string? AdditionalInfo { get; set; }
    public string? ExternalId { get; set; }
    public string? SourceRecId { get; set; }
    public int? BinaryCheckSum { get; set; }
    public double? RotationAngle { get; set; }
}
