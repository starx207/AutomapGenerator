using System.Runtime.CompilerServices;
using AutomapGenerator.SourceGenerator;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis;
using XUnitVerifier = VerifyXunit.Verifier;

namespace AutomapGenerator.Generator.VerificationTests;
internal static class Verifier {
    public static Task Verify(string source, string directory, [CallerMemberName] string? testName = null) => Verify(new[] { source }, directory, testName);

    public static Task Verify(IEnumerable<string> sources, string directory, [CallerMemberName] string? testName = null) {
        var syntaxTrees = sources.Select(s => CSharpSyntaxTree.ParseText(s)).ToArray();
        var references = new[] {
            MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
            MetadataReference.CreateFromFile(typeof(MapProfile).Assembly.Location)
        };

        var compilation = CSharpCompilation.Create(
            assemblyName: nameof(MapperGenerator_CreateMapVerifications),
            syntaxTrees: syntaxTrees,
            references: references);

        var generator = new MapperGenerator();
        GeneratorDriver driver = CSharpGeneratorDriver.Create(generator);

        driver = driver.RunGenerators(compilation);

        return XUnitVerifier.Verify(driver)
            .UseDirectory(directory)
            .UseFileName(testName!);
    }
}
