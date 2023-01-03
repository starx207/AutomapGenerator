using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace AutomapGenerator.SourceGenerator.Internal;

internal static class MapGenHelper {
    private const string MAP_METHOD_NAME = "Map";
    private const string GENERIC_TYPE_NAME_DESTINATION = "TDestination";
    private const string MAPPER_INTERFACE = "IMapper";

    public static NamespaceDeclarationSyntax CreateMapperInterface() 
        => CreateCoreNamespace()
            .WithMembers(
                SingletonList<MemberDeclarationSyntax>(
                    InterfaceDeclaration(MAPPER_INTERFACE)
                    .WithModifiers(
                        TokenList(
                            Token(SyntaxKind.PublicKeyword)))
                    .WithMembers(List(new MemberDeclarationSyntax[] {
                        CreateMapNewSignature("source").WithSemicolonToken(Token(SyntaxKind.SemicolonToken)),
                        CreateMapExistingSignature("source", "destination").WithSemicolonToken(Token(SyntaxKind.SemicolonToken))
                    }))))
            .NormalizeWhitespace();

    public static NamespaceDeclarationSyntax CreateMapperClass(MapDefinition[] sources) {
        var mapperMethods = new List<MemberDeclarationSyntax> {
            CreateMapNewMethod(),
            CreateMapExistingMethod(sources)
        };

        return CreateCoreNamespace()
        .WithMembers(
            SingletonList<MemberDeclarationSyntax>(
                ClassDeclaration("Mapper")
                .WithModifiers(
                    TokenList(
                        Token(SyntaxKind.PublicKeyword)))
                .WithBaseList(
                    BaseList(
                        SingletonSeparatedList<BaseTypeSyntax>(
                            SimpleBaseType(IdentifierName(MAPPER_INTERFACE)))))
                .WithMembers(List(mapperMethods))))
        .NormalizeWhitespace();
    }

    private static MethodDeclarationSyntax CreateMapNewSignature(string sourceParamName)
        => MethodDeclaration(
            IdentifierName(GENERIC_TYPE_NAME_DESTINATION),
            Identifier(MAP_METHOD_NAME))
        .WithTypeParameterList(
            TypeParameterList(
                SingletonSeparatedList(
                    TypeParameter(Identifier(GENERIC_TYPE_NAME_DESTINATION)))))
        .WithParameterList(
            ParameterList(
                SingletonSeparatedList(
                    Parameter(
                        Identifier(sourceParamName))
                    .WithType(PredefinedType(Token(SyntaxKind.ObjectKeyword))))))
        .WithConstraintClauses(
            SingletonList(
                TypeParameterConstraintClause(
                    IdentifierName(GENERIC_TYPE_NAME_DESTINATION))
                .WithConstraints(
                    SingletonSeparatedList<TypeParameterConstraintSyntax>(
                        ConstructorConstraint()))));

    private static MethodDeclarationSyntax CreateMapExistingSignature(string sourceParamName, string destinationParamName)
        => MethodDeclaration(
            IdentifierName(GENERIC_TYPE_NAME_DESTINATION),
            Identifier(MAP_METHOD_NAME))
        .WithTypeParameterList(
            TypeParameterList(
                SingletonSeparatedList(
                    TypeParameter(
                        Identifier(GENERIC_TYPE_NAME_DESTINATION)))))
        .WithParameterList(
            ParameterList(
                SeparatedList<ParameterSyntax>(new SyntaxNodeOrToken[] {
                    Parameter(Identifier(sourceParamName)).WithType(PredefinedType(Token(SyntaxKind.ObjectKeyword))),
                    Token(SyntaxKind.CommaToken),
                    Parameter(Identifier(destinationParamName)).WithType(IdentifierName(GENERIC_TYPE_NAME_DESTINATION))
                })));

    private static NamespaceDeclarationSyntax CreateCoreNamespace()
        => NamespaceDeclaration(
            //IdentifierName(typeof(MappingException).Namespace)) // TODO: This currently does not work in consuming project. Need to investigate why
            IdentifierName("AutomapGenerator"));

    private static MethodDeclarationSyntax CreateMapNewMethod() {
        var sourceVarName = "source";

        return CreateMapNewSignature(sourceVarName)
            .WithModifiers(
                TokenList(
                    Token(SyntaxKind.PublicKeyword)))
            .WithExpressionBody(
                ArrowExpressionClause(
                    InvocationExpression(
                        GenericName(
                            Identifier(MAP_METHOD_NAME))
                        .WithTypeArgumentList(
                            TypeArgumentList(
                                SingletonSeparatedList<TypeSyntax>(IdentifierName(GENERIC_TYPE_NAME_DESTINATION)))))
                    .WithArgumentList(
                        ArgumentList(
                            SeparatedList<ArgumentSyntax>(new SyntaxNodeOrToken[] {
                                Argument(IdentifierName(sourceVarName)),
                                Token(SyntaxKind.CommaToken),
                                Argument(
                                    ObjectCreationExpression(IdentifierName(GENERIC_TYPE_NAME_DESTINATION))
                                    .WithArgumentList(ArgumentList()))
                            })))))
            .WithSemicolonToken(Token(SyntaxKind.SemicolonToken));
    }

    private static MethodDeclarationSyntax CreateMapExistingMethod(MapDefinition[] srcMappings) {
        var destinationVarName = "destination";
        var sourceVarName = "source";

        var switchSections = new List<SwitchSectionSyntax>();
        for (var i = 0; i < srcMappings.Length; i++) {
            var mapping = srcMappings[i];
            switchSections.Add(CreateSwitchSection(mapping));
        }

        // Add the default switch section 
        switchSections.Add(
            SwitchSection()
            .WithLabels(SingletonList<SwitchLabelSyntax>(DefaultSwitchLabel()))
            .WithStatements(SingletonList<StatementSyntax>(
                ThrowStatement(
                    ObjectCreationExpression(
                        //IdentifierName(nameof(MappingException))) // TODO: This currently does not work in consuming project. Need to investigate why
                        IdentifierName("MappingException"))
                    .WithArgumentList(
                        ArgumentList(
                            SingletonSeparatedList(
                                Argument(
                                    InterpolatedStringExpression(
                                          Token(SyntaxKind.InterpolatedStringStartToken))
                                    .WithContents(List(new InterpolatedStringContentSyntax[] {
                                        InterpolatedStringText()
                                        .WithTextToken(Token(
                                            TriviaList(),
                                            SyntaxKind.InterpolatedStringTextToken,
                                            "Mapping from ",
                                            "Mapping from ",
                                            TriviaList())),
                                        Interpolation(MemberAccessExpression(
                                            SyntaxKind.SimpleMemberAccessExpression,
                                            InvocationExpression(
                                                MemberAccessExpression(
                                                    SyntaxKind.SimpleMemberAccessExpression,
                                                    IdentifierName(sourceVarName),
                                                    IdentifierName("GetType"))),
                                            IdentifierName("Name"))),
                                        InterpolatedStringText()
                                        .WithTextToken(Token(
                                            TriviaList(),
                                            SyntaxKind.InterpolatedStringTextToken,
                                            " to ",
                                            " to ",
                                            TriviaList())),
                                        Interpolation(
                                            MemberAccessExpression(
                                                SyntaxKind.SimpleMemberAccessExpression,
                                                TypeOfExpression(
                                                    IdentifierName(GENERIC_TYPE_NAME_DESTINATION)),
                                                IdentifierName("Name"))),
                                        InterpolatedStringText()
                                        .WithTextToken(Token(
                                            TriviaList(),
                                            SyntaxKind.InterpolatedStringTextToken,
                                            " has not been configured.",
                                            " has not been configured.",
                                            TriviaList()))
                                    })))))))))
        );

        return CreateMapExistingSignature(sourceVarName, destinationVarName)
            .WithModifiers(
                TokenList(
                    Token(SyntaxKind.PublicKeyword)))
            .WithBody(
                Block(
                    SwitchStatement(
                        TupleExpression(
                            SeparatedList<ArgumentSyntax>(new SyntaxNodeOrToken[] {
                                Argument(IdentifierName(sourceVarName)),
                                Token(SyntaxKind.CommaToken),
                                Argument(IdentifierName(destinationVarName))
                            })))
                    .WithSections(List(switchSections)),
                    ReturnStatement(IdentifierName(destinationVarName))
                ));
    }

    private static SwitchSectionSyntax CreateSwitchSection(MapDefinition mapping) {
        var patternMatchSrcName = ParseTypeName(mapping.SourceName);
        var patternMatchDestName = ParseTypeName(mapping.DestinationName);
        var matchedSrcVarName = "s";
        var matchedDestVarName = "d";

        var expressions = new List<StatementSyntax>();
        for (var i = 0; i < mapping.WritableDestinationProperties.Length; i++) {
            var destProp = mapping.WritableDestinationProperties[i];
            var sourceProp = mapping.SourceProperties.Single(p => p.Name == destProp.Name);

            expressions.Add(ExpressionStatement(
                AssignmentExpression(
                    SyntaxKind.SimpleAssignmentExpression,
                    MemberAccessExpression(
                        SyntaxKind.SimpleMemberAccessExpression,
                        IdentifierName(matchedDestVarName),
                        IdentifierName(destProp.Name)),
                    MemberAccessExpression(
                        SyntaxKind.SimpleMemberAccessExpression,
                        IdentifierName(matchedSrcVarName),
                        IdentifierName(sourceProp.Name)))));
        }
        expressions.Add(BreakStatement());

        return SwitchSection()
            .WithLabels(
                SingletonList<SwitchLabelSyntax>(
                    CasePatternSwitchLabel(
                        RecursivePattern()
                        .WithPositionalPatternClause(
                            PositionalPatternClause(
                                SeparatedList<SubpatternSyntax>(new SyntaxNodeOrToken[] {
                                    Subpattern(
                                        DeclarationPattern(
                                            patternMatchSrcName,
                                            SingleVariableDesignation(Identifier(matchedSrcVarName)))),
                                    Token(SyntaxKind.CommaToken),
                                    Subpattern(
                                        DeclarationPattern(
                                            patternMatchDestName,
                                            SingleVariableDesignation(Identifier(matchedDestVarName))))
                                }))),
                        Token(SyntaxKind.ColonToken))))
            .WithStatements(List(expressions));
    }
}
