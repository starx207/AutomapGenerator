namespace AutomapGenerator.FunctionalTests.Models;
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
