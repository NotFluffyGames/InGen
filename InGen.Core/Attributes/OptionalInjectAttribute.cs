using System;
using System.Diagnostics;

namespace InGen;

[Conditional("DEBUG")]
[AttributeUsage(AttributeTargets.Field 
                | AttributeTargets.Property 
                | AttributeTargets.Method)]
public class OptionalInjectAttribute : Attribute
{
    public readonly object? Id;

    public OptionalInjectAttribute(object? id = null)
    {
        Id = id;
    }
}