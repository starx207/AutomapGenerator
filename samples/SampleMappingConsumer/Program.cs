using System.Text.Json;
using AutomapGenerator;
using SampleMappingConsumer.Models;

var src = new SourceObj() {
    Id = Guid.NewGuid(),
    Type = "the other source",
    Timestamp = DateTime.Parse("5/23/2020"),
    InUse = false
};

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
