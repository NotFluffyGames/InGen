namespace InGen.Container.Generator;

[Generator]
public class Generator : ISourceGenerator
{
    public void Initialize(GeneratorInitializationContext context)
    {
        context.RegisterForSyntaxNotifications(() => new GeneratorSyntaxReceiver());
    }

    public void Execute(GeneratorExecutionContext context)
    {
        new Roslyn3ContainerSourceGenerator(context).Run();
    }
}