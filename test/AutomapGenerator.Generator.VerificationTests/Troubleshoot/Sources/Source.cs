using System;
using System.Collections.Generic;

namespace AutomapGenerator.Generator.VerificationTests.Troubleshoot.Sources;
public class TestSource : ISourceFile {
    public string GetSourceFilePath() => SourceReader.WhereAmI();
}

public class DocMetaData : AuditEntity {
    public Guid Id { get; set; }
    public string? DocTitle { get; set; }
    public string? DocUser { get; set; }
    public DateTime? DocDate { get; set; }
    public DateTime? TimeStamp { get; set; }
    public int? DocDateTypeIdx { get; set; }
    public DateTime? DeleteDate { get; set; }
    public long? Bcs { get; set; }
    public long? DocLen { get; set; }
    public int? InstanceNameId { get; set; }
    public DateTime? DateTimeAddedToMaster { get; set; }
    public DocType? Type { get; set; }
    public Document DocContent { get; set; } = new();
    public ICollection<Keyword> Keywords { get; set; } = new HashSet<Keyword>();
}

public class Document {
    public byte[] DocBinary { get; set; } = Array.Empty<byte>();
}

public class Keyword {
    public int Id { get; set; }
    public string Word { get; set; }

    [Obsolete("This is only to add AutoMapper support. Use parameterized constructor instead.", true)]
    public Keyword() : this(default, string.Empty) {
    }
    public Keyword(string word) : this(default, word) {
    }
    public Keyword(int id, string word) {
        Id = id;
        Word = word;
    }

    public override string ToString() => Word;
}

public class DocType {
    public string Code { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int SortIndex { get; set; }
}

public abstract class AuditEntity {
    public DateTime CreateDate { get; set; }
    public DateTime ChangeDate { get; set; }
    public User? CreateUser { get; set; }
    public User? ChangeUser { get; set; }
}

public class User {
    public string UserName { get; set; } = string.Empty;
}
