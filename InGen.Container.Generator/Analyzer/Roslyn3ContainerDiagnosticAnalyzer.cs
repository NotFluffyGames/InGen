using Microsoft.CodeAnalysis.Diagnostics;

namespace InGen.Generator;

public class Roslyn3ContainerDiagnosticAnalyzer
{
    private readonly CompilationAnalysisContext _compilationContext;
    private readonly AnalyzerSyntaxReceiver _syntaxCollector;

    public Roslyn3ContainerDiagnosticAnalyzer(CompilationAnalysisContext compilationContext, AnalyzerSyntaxReceiver syntaxCollector)
    {
        _compilationContext = compilationContext;
        _syntaxCollector = syntaxCollector;
    }

    public void Run()
    {
    }
}