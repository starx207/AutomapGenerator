using System.Runtime.CompilerServices;
using AutomapGenerator.SourceGenerator;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace AutomapGenerator.Generator.VerificationTests;

[UsesVerify]
public class MapperGeneratorVerifications {
    #region Tests
    
    [Fact]
    public Task SimpleObjectMap() {
        var objects = @"
namespace SampleMappingConsumer.Models;

public class SourceObj {
    public Guid Id { get; set; }
    public string? Type { get; set; }
    public DateTime? Timestamp { get; set; }
    public bool InUse { get; set; }
}


public class DestinationObj {
    public Guid Id { get; set; }
    public string? Type { get; set; }
    public DateTime? Timestamp { get; set; }
    public bool InUse { get; set; }
}";

        var mapper = @"
using AutomapGenerator;
using SampleMappingConsumer.Models;

namespace SampleMappingConsumer.Mappings;
public class BasicMapProfile : MapProfile {
    public BasicMapProfile() {
        CreateMap<SourceObj, DestinationObj>();
    }
}";

        return Verify(new[] { objects, mapper });
    }

    [Fact]
    public Task MultipleDestinationMap() {
        var objects = @"
namespace SampleMappingConsumer.Models;

public class SourceObj {
    public Guid Id { get; set; }
    public string? Type { get; set; }
}


public class Destination1Obj {
    public Guid Id { get; set; }
    public string? Type { get; set; }
}

public class Destination2Obj {
    public Guid Id { get; set; }
    public string? Type { get; set; }
}";

        var mapper = @"
using AutomapGenerator;
using SampleMappingConsumer.Models;

namespace SampleMappingConsumer.Mappings;
public class BasicMapProfile : MapProfile {
    public BasicMapProfile() {
        CreateMap<SourceObj, Destination1Obj>();
        CreateMap<SourceObj, Destination2Obj>();
    }
}";

        return Verify(new[] { objects, mapper });
    }

    [Fact]
    public Task MultipleSourceMap() {
        var objects = @"
namespace SampleMappingConsumer.Models;

public class Source1Obj {
    public Guid Id { get; set; }
    public string? Type { get; set; }
}


public class Source2Obj {
    public Guid Id { get; set; }
    public string? Type { get; set; }
}


public class DestinationObj {
    public Guid Id { get; set; }
    public string? Type { get; set; }
}";

        var mapper = @"
using AutomapGenerator;
using SampleMappingConsumer.Models;

namespace SampleMappingConsumer.Mappings;
public class BasicMapProfile : MapProfile {
    public BasicMapProfile() {
        CreateMap<Source1Obj, DestinationObj>();
        CreateMap<Source2Obj, DestinationObj>();
    }
}";

        return Verify(new[] { objects, mapper });
    }

    [Fact]
    public Task MapperWithEmptyConstructor() {
        var mapper = @"
using AutomapGenerator;

namespace SampleMappingConsumer.Mappings;
public class EmptyMapProfile : MapProfile {
    public EmptyMapProfile() {
    }
}";

        return Verify(mapper);
    }

    [Fact]
    public Task MapperWithNoConstructor() {
        var mapper = @"
using AutomapGenerator;

namespace SampleMappingConsumer.Mappings;
public class EmptyMapProfile : MapProfile {
}";

        return Verify(mapper);
    }

    [Fact]
    public Task SimpleObjectMap_WithArrowFunction() {
        var objects = @"
namespace SampleMappingConsumer.Models;

public class SourceObj {
    public Guid Id { get; set; }
    public string? Type { get; set; }
    public DateTime? Timestamp { get; set; }
    public bool InUse { get; set; }
}


public class DestinationObj {
    public Guid Id { get; set; }
    public string? Type { get; set; }
    public DateTime? Timestamp { get; set; }
    public bool InUse { get; set; }
}";

        var mapper = @"
using AutomapGenerator;
using SampleMappingConsumer.Models;

namespace SampleMappingConsumer.Mappings;
public class BasicMapProfile : MapProfile {
    public BasicMapProfile() => CreateMap<SourceObj, DestinationObj>();
}";

        return Verify(new[] { objects, mapper });
    }

    [Fact]
    public Task MapWithReadonlyDestinations() {
        var objects = @"
namespace SampleMappingConsumer.Models;

public class SourceObj {
    public Guid Id { get; set; }
    public string? Type { get; set; }
    public DateTime? Timestamp { get; set; }
    public bool InUse { get; set; }
}


public class DestinationObj {
    public Guid Id { get; set; }
    public string? Type { get; }
    public DateTime? Timestamp { get; set; }
    public bool InUse { get; set; }
}";

        var mapper = @"
using AutomapGenerator;
using SampleMappingConsumer.Models;

namespace SampleMappingConsumer.Mappings;
public class BasicMapProfile : MapProfile {
    public BasicMapProfile() {
        CreateMap<SourceObj, DestinationObj>();
    }
}";

        return Verify(new[] { objects, mapper });
    }

    [Fact]
    public Task MapWithReadonlySources() {
        var objects = @"
namespace SampleMappingConsumer.Models;

public class SourceObj {
    public Guid Id { get; set; }
    public string? Type { get; set; }
    public DateTime? Timestamp { get; set; }
    public bool InUse { get; }
}


public class DestinationObj {
    public Guid Id { get; set; }
    public string? Type { get; set; }
    public DateTime? Timestamp { get; set; }
    public bool InUse { get; set; }
}";

        var mapper = @"
using AutomapGenerator;
using SampleMappingConsumer.Models;

namespace SampleMappingConsumer.Mappings;
public class BasicMapProfile : MapProfile {
    public BasicMapProfile() {
        CreateMap<SourceObj, DestinationObj>();
    }
}";

        return Verify(new[] { objects, mapper });
    }

    #endregion

    #region TestHelpers

    private static Task Verify(string source, [CallerMemberName] string? testName = null) => Verify(new[] { source }, testName);

    private static Task Verify(IEnumerable<string> sources, [CallerMemberName] string? testName = null) {
        var syntaxTrees = sources.Select(s => CSharpSyntaxTree.ParseText(s)).ToArray();
        var references = new[] {
            MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
            MetadataReference.CreateFromFile(typeof(MapProfile).Assembly.Location)
        };

        var compilation = CSharpCompilation.Create(
            assemblyName: nameof(MapperGeneratorVerifications),
            syntaxTrees: syntaxTrees,
            references: references);

        var generator = new MapperGenerator();
        GeneratorDriver driver = CSharpGeneratorDriver.Create(generator);

        driver = driver.RunGenerators(compilation);

        return Verifier.Verify(driver)
            .UseDirectory(@"Snapshots\MapperGenerator")
            .UseFileName(testName!);
    }

    #endregion
}
