using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AutomapGenerator.SourceGenerator.Internal;
internal readonly struct MappingCustomization {
    public bool Ignore { get; }
    public SimpleLambdaExpressionSyntax? LambdaMapping { get; }

    public MappingCustomization() { }
    public MappingCustomization(bool ignore) => Ignore = ignore;
    public MappingCustomization(SimpleLambdaExpressionSyntax lambdaMapping) => LambdaMapping = lambdaMapping;
}
