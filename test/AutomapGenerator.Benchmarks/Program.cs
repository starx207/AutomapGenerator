using System.Reflection;
using BenchmarkDotNet.Running;

#if !DEBUG
BenchmarkRunner.Run(Assembly.GetExecutingAssembly(), args: args);
return;
#endif


// TODO: Perhaps I can do a test in this project to see how Automapper handles mapping to a struct
//       Mapping to new instance shouldn't be a problem - but what about mapping to existing instance?
//       While I'm at it, what does it do when it doesn't know how to construct an object when mapping to new?

/*
 * Results:
 *     String mappings: No profile required for string mappings.
 *                      Mapping to existing string returns the mapped string but does NOT mutate the input destination.
 *                      When the source is a string, AutoMapper will attempt to parse the string to the desired destination
 *                        -> ex. string to bool, string to int, string to date, etc. (Not everything with a Parse method is supported though. Like DateOnly as an example)
 *     
 *     Struct mappings: Works like strings (profile required).
 *                      Mapping to existing struct returns the mapped struct, but does NOT mutate the input destination.
 *                      Only maps writable properties unless constructor parameter name matches property name (case-insensitive)
 *                       -> The constructor is only used if ALL parameters can be mapped. If 1 doesn't match, the constructor is not used
 *                       
 *     Constructors in general: See constructor note above.
 *                              Once a source property is used in the constructor, it is not used again to set a property (i.e. setting something via the constructor
 *                              does NOT reset it through the property setter) - this only applies to intrinsic constructor mappings, NOT explicit constructor mappings
 */

var autoMap = new AutoMapper.MapperConfiguration(cfg => {
    cfg.CreateMap<Keyword, OtherStruct>().ConstructUsing(src => new OtherStruct(src.Text));
    cfg.CreateMap<Keyword, ReadonlyStruct>();
    cfg.CreateMap<string, DateOnly>().ConstructUsing(src => DateOnly.Parse(src));
}).CreateMapper();


var keyword = new Keyword() { Id = 23, Text = "Hello, World!" };
var otherCls = new OtherStruct("Very nice, how are ya?") { Id = 9 };
var rstruct = new ReadonlyStruct(8, "Good afternoon");
var str = "Goodbye, World :(";

var test = autoMap.Map(keyword, str);
var test2 = autoMap.Map(keyword, otherCls);
var test3 = autoMap.Map(keyword, rstruct);
var test4 = autoMap.Map<OtherStruct>(keyword);
var test5 = autoMap.Map<string>((Keyword?)null);
var test6 = autoMap.Map<DateOnly>("7/2/2024");
var test7 = autoMap.Map<DateTime>("1/31/2024");

Console.WriteLine(str);
Console.WriteLine(test);

Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(otherCls));
Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(test2));

Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(rstruct));
Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(test3));

Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(test4));
Console.WriteLine(test5 ?? "_null_");
Console.WriteLine(test6);
Console.WriteLine(test7);


class Keyword {
    public int Id { get; set; }
    public string Text { get; set; } = string.Empty;

    public override string ToString() => Text;
}

class OtherStruct {
    private string _text;

    public int Id { get; set; }
    public string Text { get => _text; set => _text = value + " from property setter"; }

    public OtherStruct() : this(string.Empty) {
        
    }

    public OtherStruct(string input) {
        _text = input;
        if (!string.IsNullOrEmpty(_text)) {
            _text += " from constructor";
        }
    }
}

readonly struct ReadonlyStruct {
    public int Id { get; }
    public string Text { get; }

    public ReadonlyStruct(int id, string text2) {
        Id = id;
        Text = text2;
    }
}
