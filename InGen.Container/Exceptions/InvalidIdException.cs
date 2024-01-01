using System;

namespace InGen.Exceptions;

public class InvalidIdException : Exception
{
    public readonly IContainer Container;
    public readonly Type ResolveType;
    public readonly object? Id;

    public InvalidIdException(IContainer container, Type resolveType, object? id)
    {
        Container = container;
        ResolveType = resolveType;
        Id = id;
    }
}