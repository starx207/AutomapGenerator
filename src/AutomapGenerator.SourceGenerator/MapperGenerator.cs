using System.Text;
using AutomapGenerator.SourceGenerator.Internal;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AutomapGenerator.SourceGenerator;

[Generator]
public class MapperGenerator : IIncrementalGenerator {
    public void Initialize(IncrementalGeneratorInitializationContext context) {
        var mapInvocations = context.SyntaxProvider
            .CreateSyntaxProvider(
                predicate: static (m, _) => IsSyntaxCandidateForMapping(m),
                transform: static (ctx, tok) => MapperSyntaxParser.GetMappingProfileDeclaration(ctx, tok))
            .Where(static m => m is not null)
            .SelectMany(static (m, _) => MapperSyntaxParser.ExtractConstructorInvocations(m!));

        var mapDefinitions = context.CompilationProvider.Combine(mapInvocations.Collect())
            .Select(static (src, tok) => MapDefinitionHelper.ConvertToMapDefinitions(src.Left, src.Right, tok));

        context.RegisterSourceOutput(mapDefinitions,
            static (spc, source) => Execute(source, spc));
    }

    private static void Execute(IEnumerable<MapDefinition> defs, SourceProductionContext spc) {
        if (defs.Any()) {
            var mapperOutput = MapGenHelper.CreateMapperClass(defs.ToArray());
            var interfaceOutput = MapGenHelper.CreateMapperInterface();

            spc.AddSource("Mapper.g.cs", mapperOutput.GetText(Encoding.UTF8));
            spc.AddSource("IMapper.g.cs", interfaceOutput.GetText(Encoding.UTF8));
        }
    }

    private static bool IsSyntaxCandidateForMapping(SyntaxNode m)
        => m is ClassDeclarationSyntax { BaseList.Types.Count: > 0, Members.Count: > 0 };
}
