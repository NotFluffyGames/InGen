using System;
using System.Diagnostics;

namespace InGen;

[Conditional("DEBUG")]
[AttributeUsage(AttributeTargets.Constructor 
                | AttributeTargets.Field 
                | AttributeTargets.Property 
                | AttributeTargets.Method)]
public class InjectAttribute : Attribute
{
    public readonly object? Id;

    public InjectAttribute(object? id = null)
    {
        Id = id;
    }
}