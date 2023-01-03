namespace SampleMappingConsumer.Models;
public class DestinationObj {
    public Guid Id { get; set; }
    public string? Type { get; set; }
    public DateTime? Timestamp { get; set; }
    public bool InUse { get; set; }
}
