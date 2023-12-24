using System;
using System.Diagnostics;

namespace InGen;

[Conditional("DEBUG")]
[AttributeUsage(AttributeTargets.Parameter, AllowMultiple = true)]
public class WithIdAttribute : Attribute
{
    public readonly object Id;

    public WithIdAttribute(object id)
    {
        Id = id;
    }
}