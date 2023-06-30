using System.Collections.Immutable;
using System.Text;
using AutomapGenerator.SourceGenerator.Internal;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AutomapGenerator.SourceGenerator;

// TODO: I recently learned that for vs foreach performance is nearly identical for arrays, so anywhere I'm using arrays, I could simplify my approach
//       to use foreach. Lists however, are twice as slow with foreach (in .NET 6). I need to investigate the other types I'm iterating. Is there
//       a performance difference with ImmutableArrays? .NET 7 actually degrades the performance of for loops for lists in order to improve the performance
//       of foreach! (https://www.youtube.com/watch?v=LfgBm5M8eUM). Not sure if the same is true in .NET 8
//
//       However, I just remembered that source generators are constrained to targeting netstandard2.0! So everything I said is moot. Although I would
//       still like to do some investigation on the performance of the different types of loops vs LINQ with the different types of IEnumerables that
//       I'm using to see if I can simplify any code without losing performance.


// TODO: .NET 8 / C# 12 will add the ability to define "Interceptors". I should investigate this to see if that might be a better way to generate what I need.
//       It's possible that I could generate interceptors for each combination of types that would intercept
//       all my IMapper calls. Might have much more simplicity
//       than trying to have that massive switch statment based on the types.
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
