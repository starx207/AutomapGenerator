namespace AutomapGenerator.FunctionalTests.Models;
public class DocumentDownloadViewModel {
    public string FileName { get; set; }
    public string FileFormat { get; set; }
    public byte[] Content { get; set; }

    public DocumentDownloadViewModel() {
        FileName = string.Empty;
        FileFormat = string.Empty;
        Content = Array.Empty<byte>();
    }
}
