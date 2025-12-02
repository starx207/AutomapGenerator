using System;
using System.Collections.Generic;

namespace AutomapGenerator.Generator.VerificationTests.Troubleshoot.Sources;
public class TestDestination : ISourceFile {
    public string GetSourceFilePath() => SourceReader.WhereAmI();
}

public class DocSearchViewModel {
    public Guid Id { get; set; }
    public string? DocTitle { get; set; }
    public string? Creator { get; set; }
    public DateTime? CreateDate { get; set; }
    public DateTime? ChangeDate { get; set; }
    public DateTime? DocDeleteDate { get; set; }
    public DateTime? DocDate { get; set; }
    public long? Length { get; set; }
    public string? Type { get; set; }
    public int? SortIndex { get; set; }
}

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

public class ModifyDocPatch {
    public string? DocTitle { get; set; }
    public DateTime? DocDate { get; set; }
    //public IEnumerable<string>? Keywords { get; set; }
}

public class MoveDocPatch {
    public string? NewDocTitle { get; set; }
    public string? NewTypeCode { get; set; }
}

public class CreateDocumentCommand {
    public DateTime? DocDate { get; set; }
    public string? DocType { get; set; }
    public IEnumerable<string>? Keywords { get; set; }
    public string? Format { get; set; }
    public string? FileName { get; set; }
    public byte[]? Content { get; set; }
}

public class NewDocumentInput {
    public string? DocType { get; set; }
    public DateTime? DocDate { get; set; }
    public string[]? Keywords { get; set; }
    public string? Format { get; set; }
    public IFormFile? File { get; set; }
}

public interface IFormFile {
    string Name { get; set; }
}
