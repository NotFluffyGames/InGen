using System;
using System.Reflection;

namespace InGen.Exceptions;

public class InvalidTypeException : Exception
{
    public readonly IContainer Container;
    public readonly Type ResolveType;
    public readonly object? Id;

    public InvalidTypeException(IContainer container, Type resolveType, object? id = null)
        : base(GetMessage(container, resolveType, id))
    {
        Container = container;
        ResolveType = resolveType;
        Id = id;
    }

    private static string GetMessage(IContainer container, MemberInfo resolveType, object? id)
    {
        var msg = $"Failed to resolve type {resolveType.Name} from container {container.GetType().Name}";

        if (id != null)
            msg += $" with id {id}";

        return msg;
    }
}