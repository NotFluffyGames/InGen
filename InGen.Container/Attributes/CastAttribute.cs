using System;
using System.Diagnostics;

namespace InGen.Container;

[Conditional("DEBUG")]
[AttributeUsage(AttributeTargets.Class)]
public class CastAttribute : Attribute
{
    public CastAttribute(Type from, params Type[] to)
    {
        From = from;
        To = to;
    }

    public Type From { get; }
    public Type[] To { get; }
    
    public object? Id { get; set; } 
}