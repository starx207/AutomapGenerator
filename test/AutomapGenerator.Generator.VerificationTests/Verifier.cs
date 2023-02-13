﻿using System.Runtime.CompilerServices;
using AutomapGenerator.SourceGenerator;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis;
using XUnitVerifier = VerifyXunit.Verifier;

namespace AutomapGenerator.Generator.VerificationTests;
internal static class Verifier {
    private static readonly string _coreLibPath = Path.Combine(Path.GetDirectoryName(typeof(object).Assembly.Location)!, "{0}.dll");
    private static readonly MetadataReference[] _references = new[] {
            MetadataReference.CreateFromFile(string.Format(_coreLibPath, "mscorlib")),
            MetadataReference.CreateFromFile(string.Format(_coreLibPath, "System")),
            MetadataReference.CreateFromFile(string.Format(_coreLibPath, "System.Core")),
            MetadataReference.CreateFromFile(string.Format(_coreLibPath, "System.Private.CoreLib")),
            MetadataReference.CreateFromFile(string.Format(_coreLibPath, "System.Runtime")),
            MetadataReference.CreateFromFile(string.Format(_coreLibPath, "System.Linq.Expressions")),
            MetadataReference.CreateFromFile(typeof(MapProfile).Assembly.Location)
        };

    public static Task Verify(string source, string directory, [CallerMemberName] string? testName = null) => Verify(new[] { source }, directory, testName);

    public static Task Verify(IEnumerable<string> sources, string directory, [CallerMemberName] string? testName = null) {
        var syntaxTrees = sources.Select(s => CSharpSyntaxTree.ParseText(s)).ToArray();

        var compilation = CSharpCompilation.Create(
            assemblyName: "Mapper_Verifications",
            syntaxTrees: syntaxTrees,
            references: _references);

        var generator = new MapperGenerator();
        GeneratorDriver driver = CSharpGeneratorDriver.Create(generator);

        driver = driver.RunGenerators(compilation);

        return XUnitVerifier.Verify(driver)
            .UseDirectory(directory)
            .UseFileName(testName!);
    }
}
