namespace InGen.Container.Generator;

internal static partial class DiagnosticDescriptors
{
    public static readonly DiagnosticDescriptor UnexpectedErrorDescriptor = Create(
        1,
        "Unexpected error during generation",
        "Unexpected error occurred during code generation: {0}", 
        DiagnosticSeverity.Error);
}