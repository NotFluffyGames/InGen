using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace InGen.Container.Generator;

public class AnalyzerSyntaxReceiver : ISyntaxReceiver
{
    public readonly List<SyntaxNode> Nodes = new();
    
    public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
    {
        if (syntaxNode is ClassDeclarationSyntax)
            Nodes.Add(syntaxNode);
    }
}