using System;
using Microsoft.CodeAnalysis.Diagnostics;

namespace InGen.Container.Generator;

internal static partial class DiagnosticDescriptors
{
    public const string Category = "InGen.Container";
    public static string GetId(uint code) => $"ING{code:D4}";
        
    public static DiagnosticDescriptor Create(
        uint id, 
        string title, 
        string messageFormat, 
        DiagnosticSeverity diagnosticSeverity,
        string? description = null,
        string? helpLinkUri = null,
        params string[] customTags)
    {
        return new(
            GetId(id),
            title,
            messageFormat,
            Category,
            diagnosticSeverity,
            true,
            description,
            helpLinkUri,
            customTags);
    }

    public static Diagnostic ToDiagnostic(this Exception exception, Location? location)
    {
        return Diagnostic.Create(UnexpectedErrorDescriptor, location, exception.ToString());
    }

    public static void ReportTo(this Diagnostic diagnostic, CompilationAnalysisContext context)
        => context.ReportDiagnostic(diagnostic);
    
    public static void ReportTo(this Diagnostic diagnostic, GeneratorExecutionContext context)
        => context.ReportDiagnostic(diagnostic);
    
    //For Roslyn 4+
    // public static void ReportTo(this Diagnostic diagnostic, SourceProductionContext context)
    //     => context.ReportDiagnostic(diagnostic);
}