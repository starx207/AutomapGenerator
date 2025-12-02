using System.Text.Json;
using AutomapGenerator;
using SampleMappingConsumer.Models;

var src = new SourceObj() {
    Id = Guid.NewGuid(),
    Type = "the other source",
    Timestamp = DateTime.Parse("5/23/2020"),
    InUse = false
};

//var mapper = new Test();
IMapper mapper = new Mapper();

var dest = mapper.Map<DestinationObj>(src);

Console.WriteLine(JsonSerializer.Serialize(src));
Console.WriteLine(JsonSerializer.Serialize(dest));


var src2 = new SourceObj() {
    Id = Guid.NewGuid(),
    Type = "updated type",
    Timestamp = src.Timestamp.Value.AddDays(3),
    InUse = true
};

mapper.Map(src2, dest);

Console.WriteLine("Updated destination...");
Console.WriteLine(JsonSerializer.Serialize(dest));

//var destArray = mapper.Map<DestinationObj[]>(new[] { src });
//var destList = mapper.Map<List<DestinationObj>>(new[] { src });
//var destHash = mapper.Map<HashSet<DestinationObj>>(new[] { src });
//var destSet = mapper.Map<ISet<DestinationObj>>(new[] { src });
//var destCollection = mapper.Map<ICollection<DestinationObj>>(new[] { src });
//var destEnumerable = mapper.Map<IEnumerable<DestinationObj>>(new[] { src });

//var pause = true;


class Test {
    public TDestination Map<TDestination>(object source) {
        switch (source, typeof(TDestination)) {
            case (_, System.Type t) when t == typeof(string):
                return (dynamic)source?.ToString()!;
            case (IEnumerable<SourceObj> s, Type t) when t == typeof(DestinationObj[]):
                return (dynamic)EnumerableInternal<DestinationObj>(s).ToArray();
            case (IEnumerable<SourceObj> s, Type t) when typeof(ISet<DestinationObj>).IsAssignableFrom(t):
                return (dynamic)EnumerableInternal<DestinationObj>(s).ToHashSet();
            case (IEnumerable<SourceObj> s, Type t) when typeof(IEnumerable<DestinationObj>).IsAssignableFrom(t):
                return (dynamic)EnumerableInternal<DestinationObj>(s).ToList();
            case (SourceObj s, System.Type t) when t == typeof(DestinationObj):
                return (dynamic)MapInternal(s, new DestinationObj());
            default:
                throw new MappingException($"Mapping from {source.GetType().Name} to new {typeof(TDestination).Name} has not been configured.");
        }
    }

    public TDestination Map<TDestination>(object source, TDestination destination) {
        switch (source, destination) {
            case (_, string d):
                return (dynamic)source.ToString()!;
            case (IEnumerable<SourceObj> s, DestinationObj[]):
                return (dynamic)EnumerableInternal<DestinationObj>(s).ToArray();
            case (IEnumerable<SourceObj> s, ISet<DestinationObj>):
                return (dynamic)EnumerableInternal<DestinationObj>(s).ToHashSet();
            case (IEnumerable<SourceObj> s, IEnumerable<DestinationObj>):
                return (dynamic)EnumerableInternal<DestinationObj>(s).ToList();
            case (SourceObj s, DestinationObj d):
                return (dynamic)MapInternal(s, d);
            default:
                throw new MappingException($"Mapping from {source.GetType().Name} to existing {typeof(TDestination).Name} has not been configured.");
        }
    }

    private IEnumerable<TDestination> EnumerableInternal<TDestination>(IEnumerable<SourceObj> sourceEnumerable) {
        switch (typeof(TDestination)) {
            case Type t when t == typeof(DestinationObj):
                return Enumerable.Cast<TDestination>(
                        Enumerable.Select(sourceEnumerable, source => new DestinationObj() {
                            Id = source.Id,
                            Type = source.Type,
                            Timestamp = source.Timestamp,
                            InUse = source.InUse
                        })
                    );
            default:
                throw new MappingException($"Mapping from {sourceEnumerable.GetType().Name} to new {typeof(TDestination).Name} has not been configured.");
        }
    }

    private DestinationObj MapInternal(SourceObj src, DestinationObj dest) {
        dest.Timestamp = src.Timestamp;
        dest.Type = src.Type;
        dest.InUse = src.InUse;
        dest.Id = src.Id;

        return dest;
    }
}
