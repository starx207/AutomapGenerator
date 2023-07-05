using Microsoft.AspNetCore.Http;

namespace AutomapGenerator.FunctionalTests.Models;
public class NewDocumentInput {
    public string? DocType { get; set; }
    public DateTime? DocDate { get; set; }
    public string[]? Keywords { get; set; }
    public string? Format { get; set; }
    public IFormFile? File { get; set; }
}
