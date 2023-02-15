using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AutomapGenerator.SourceGenerator.Internal;

internal static class MapperSyntaxParser {

    public static InvocationExpressionSyntax[] ExtractConstructorInvocations(ClassDeclarationSyntax classDeclaration) {
        for (var i = 0; i < classDeclaration.Members.Count; i++) {
            var member = classDeclaration.Members[i];
            if (member is not ConstructorDeclarationSyntax { } ctor) {
                continue;
            }

            if (ctor.ParameterList is { Parameters.Count: > 0 }) {
                // TODO: Add diagnostic as I'm not planning to support injecting dependencies here
                continue;
            }

            if (ctor.ExpressionBody is { } exprBody && exprBody is { Expression: InvocationExpressionSyntax invcExpr }) {
                return new[] { invcExpr };
            }

            if (ctor.Body is { } && ctor.Body.ChildNodes().ToArray() is { Length: > 0 } nodes) {
                var invocations = new List<InvocationExpressionSyntax>();
                for (var j = 0; j < nodes.Length; j++) {
                    var node = nodes[j];
                    if (node is ExpressionStatementSyntax { Expression: InvocationExpressionSyntax bodyExpr }) {
                        invocations.Add(bodyExpr);
                    }
                }
                return invocations.ToArray();
            }
        }
        return Array.Empty<InvocationExpressionSyntax>();
    }


    public static ClassDeclarationSyntax? GetMappingProfileDeclaration(GeneratorSyntaxContext context, CancellationToken token) {
        var classDeclaration = (ClassDeclarationSyntax)context.Node;

        var distinctBases = classDeclaration.BaseList!.Types.Distinct().ToArray();
        for (var i = 0; i < distinctBases.Length; i++) {
            var baseType = distinctBases[i]!;

            if (context.SemanticModel.GetTypeInfo(baseType.Type, token).Type is not ITypeSymbol baseSymbol) {
                throw new Exception("TODO: How did we get here???");
            }

            var fullName = baseSymbol.ToDisplayString();
            // TODO: Reference the other project and get the type name here?
            if (fullName == "AutomapGenerator.MapProfile") {
                return classDeclaration;
            }
        }

        return null;
    }

}
