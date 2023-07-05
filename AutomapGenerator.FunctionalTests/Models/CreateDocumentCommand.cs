namespace AutomapGenerator.FunctionalTests.Models;
public class CreateDocumentCommand {
    public DateTime? DocDate { get; set; }
    public string? DocType { get; set; }
    public IEnumerable<string>? Keywords { get; set; }
    public string? Format { get; set; }
    public string? FileName { get; set; }
    public byte[]? Content { get; set; }
}
