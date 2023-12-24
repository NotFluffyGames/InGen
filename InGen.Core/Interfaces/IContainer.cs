using System;

namespace InGen;

public interface IContainer : IDisposable
{
    T Resolve<T>(object? id = null);
    object Resolve(Type type, object? id = null);
    
    IResult<T> TryResolve<T>(object? id = null);
    IResult<object> TryResolve(Type type, object? id = null);

    IContainer CreateScope();
}