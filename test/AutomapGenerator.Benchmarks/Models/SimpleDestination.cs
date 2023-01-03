namespace AutomapGenerator.Benchmarks.Models;
public class SimpleDestination {
    public Guid Id { get; set; }
    public string? Type { get; set; }
    public DateTime? Timestamp { get; set; }
    public bool InUse { get; set; }
}
