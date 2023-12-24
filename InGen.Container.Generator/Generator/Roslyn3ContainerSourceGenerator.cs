using System;
using System.Collections.Generic;
using CodeGenCore.Extensions;

namespace InGen.Container.Generator;

public class Roslyn3ContainerSourceGenerator
{
    private readonly GeneratorExecutionContext _context;

    public Roslyn3ContainerSourceGenerator(GeneratorExecutionContext context)
    {
        _context = context;
    }

    public void Run()
    {
        var locationTracker = new Tracker<Location>(() => Location.None);
        var disposables = new List<IDisposable>();
        
        try
        {
            var collector = (GeneratorSyntaxReceiver)_context.SyntaxReceiver!;
            
            foreach (var node in collector.Nodes)
            {
                locationTracker.Push(node.GetLocation).AddTo(disposables);

                    
                var semanticModel = _context.Compilation.GetSemanticModel(node.SyntaxTree);
                var symbol = semanticModel.GetDeclaredSymbol(node);

                if(symbol == null)
                    continue;
                
                foreach (var attribute in symbol.GetAttributes())
                {
                    var dispose = locationTracker.Push(() => attribute.ApplicationSyntaxReference?.GetSyntax().GetLocation() ?? Location.None);

                    //throw new("Attribute Test");
                    dispose?.Dispose();
                }
                
                disposables.ClearAndDispose();
            }
        }
        catch (Exception e)
        {
            e.ToDiagnostic(locationTracker.Value).ReportTo(_context);
        }
    }
}