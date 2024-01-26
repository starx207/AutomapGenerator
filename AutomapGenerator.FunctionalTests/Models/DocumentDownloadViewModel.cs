namespace AutomapGenerator.FunctionalTests.Models;
public class DocumentDownloadViewModel {
    private string _fileName;

    public string FileName {
        get => FileFormat.Length > 0 ? $"{_fileName}.{FileFormat.ToLower()}" : _fileName;
        set => _fileName = value;
    }
    public string FileFormat { get; set; }
    public byte[] Content { get; set; }

    public DocumentDownloadViewModel() {
        _fileName = string.Empty;
        FileFormat = string.Empty;
        Content = Array.Empty<byte>();
    }
}
