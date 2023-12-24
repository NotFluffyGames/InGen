using System.Collections.Immutable;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Diagnostics;

namespace InGen.Generator;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public class Analyzer : DiagnosticAnalyzer
{
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; }
    
    //Todo:
    //- No duplicate registrations in attributes (even between inherited attributes)
    //- No resolve unregistered type (or id) from type or interface implementing IContainer with attributes
    
    public override void Initialize(AnalysisContext context)
    {
        context.EnableConcurrentExecution();
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.Analyze | GeneratedCodeAnalysisFlags.ReportDiagnostics);
        context.RegisterCompilationStartAction(compilationStartAnalysisContext =>
        {
            var syntaxCollector = new AnalyzerSyntaxReceiver();
            
            compilationStartAnalysisContext.RegisterSyntaxNodeAction(
                analysisContext => syntaxCollector.OnVisitSyntaxNode(analysisContext.Node), 
                SyntaxKind.ClassDeclaration, 
                SyntaxKind.InterfaceDeclaration, 
                SyntaxKind.InvocationExpression);

            compilationStartAnalysisContext.RegisterCompilationEndAction(
                compilationContext => new Roslyn3ContainerDiagnosticAnalyzer(compilationContext, syntaxCollector)
                    .Run());
        });
    }
}