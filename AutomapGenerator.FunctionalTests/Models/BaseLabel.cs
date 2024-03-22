namespace AutomapGenerator.FunctionalTests.Models;
public abstract class BaseLabel {
    public BaseLabel(string text) => Text = text;

    public Guid Id { get; set; }
    public Guid ShapeId { get; set; }
    public string Text { get; set; }
    public string? Font { get; set; }
    public double? Size { get; set; }
    public string? Color { get; set; }
    public double Rotation { get; set; }
    public double? Transparency { get; set; }
}
public abstract class BaseLabel<TShape> : BaseLabel where TShape : BaseShape {
    public BaseLabel(string text) : base(text) => Shape = null!;

    public virtual TShape Shape { get; set; }
}
