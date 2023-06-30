using System.Collections.Immutable;
using System.Text;
using AutomapGenerator.SourceGenerator.Internal;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AutomapGenerator.SourceGenerator;

[Generator]
public class MapperGenerator : IIncrementalGenerator {
    public void Initialize(IncrementalGeneratorInitializationContext context) {
        // Get all Classes that inherit from MapProfile
        var mapProfiles = context.SyntaxProvider
            .CreateSyntaxProvider(
                predicate: static (m, _) => IsClassDeclarationSyntaxWithBase(m),
                transform: static (ctx, tok) => MapperSyntaxParser.GetMappingProfileDeclaration(ctx, tok))
            .Where(static m => m is not null)
            .Select(static (m, _) => ((SyntaxNode)m!, MapperSyntaxParser.ExtractConstructorInvocations(m!)));

        // Get all calls to IMapper.Map/ProjectTo
        var mapInvocations = context.SyntaxProvider
            .CreateSyntaxProvider(
                predicate: static (m, _) => IsMemberAccessExpressionSyntax(m),
                transform: static (ctx, tok) => MapperSyntaxParser.GetMapInvocation(ctx, tok))
            .Where(static m => m is not null)
            .Select(static (m, _) => ((SyntaxNode)m!, new[] { m! }));

        // Convert profiles to map definitions
        var mapDefinitions = context.CompilationProvider.Combine(mapProfiles.Collect())
            .Select(static (src, tok) => MapDefinitionHelper.ConvertToMapDefinitions(src.Left, src.Right, tok));

        // Convert IMapper calls to map definitions
        var adHocMapDefinitions = context.CompilationProvider.Combine(mapInvocations.Collect())
            .Select(static (src, tok) => MapDefinitionHelper.PruneAdHocDefinitions(
                MapDefinitionHelper.ConvertToMapDefinitions(src.Left, src.Right, tok)));

        // Combine the 2 sources with profiles taking precedence
        var combinedDefinitions = mapDefinitions.Combine(adHocMapDefinitions)
            .Select(static (src, _) => MapDefinitionHelper.CombineProfilesAndAdHocMappings(
                src.Left.ToImmutableArray(), 
                src.Right.ToImmutableArray()));

        // Generate the mapper from the definitions
        context.RegisterSourceOutput(combinedDefinitions,
            static (spc, source) => Execute(source, spc));
    }

    private static bool IsMemberAccessExpressionSyntax(SyntaxNode m)
        => m is InvocationExpressionSyntax { Expression: MemberAccessExpressionSyntax { Name.Identifier.Text: "Map" or "ProjectTo" } }; // { Name: { Identifier.Text: "Map" or "ProjectTo" } };

    private static void Execute(IEnumerable<MapDefinition> defs, SourceProductionContext spc) {
        if (defs.Any()) {
            var mapperOutput = MapGenHelper.CreateMapperClass(defs.ToArray());
            spc.AddSource("Mapper.g.cs", mapperOutput.GetText(Encoding.UTF8));
        }
    }

    private static bool IsClassDeclarationSyntaxWithBase(SyntaxNode m)
        => m is ClassDeclarationSyntax { BaseList.Types.Count: > 0, Members.Count: > 0 };
}
