using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace AutomapGenerator.SourceGenerator.Internal;
internal sealed class ParameterRewriter : CSharpSyntaxRewriter {
    private readonly SyntaxToken _oldParameter;
    private readonly SyntaxToken _newParameter;

    public ParameterRewriter(SyntaxToken oldParameter, SyntaxToken newParameter) {
        _oldParameter = oldParameter;
        _newParameter = newParameter;
    }

    public override SyntaxNode? VisitMemberAccessExpression(MemberAccessExpressionSyntax node)
        => node.Expression is IdentifierNameSyntax identifier && identifier.Identifier.Text == _oldParameter.Text
        ? node.WithExpression(IdentifierName(_newParameter))
        : base.VisitMemberAccessExpression(node);

    public override SyntaxNode? VisitIdentifierName(IdentifierNameSyntax node)
        => node.Identifier.Text == _oldParameter.Text
        ? node.WithIdentifier(Identifier(_newParameter.Text))
        : base.VisitIdentifierName(node);
}
