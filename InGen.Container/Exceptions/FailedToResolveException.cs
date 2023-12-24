using System;
using System.Reflection;

namespace InGen.Container.Exceptions;

public class FailedToResolveException : Exception
{
    public readonly IContainer Container;
    public readonly Type ResolveType;

    public FailedToResolveException(IContainer container, Type resolveType, object? id = null)
        : base(GetMessage(container, resolveType, id))
    {
        Container = container;
        ResolveType = resolveType;
    }

    private static string GetMessage(IContainer container, MemberInfo resolveType, object? id)
    {
        var msg = $"Failed to resolve type {resolveType.Name} from container {container.GetType().Name}";

        if (id != null)
            msg += $" with id {id}";

        return msg;
    }
}