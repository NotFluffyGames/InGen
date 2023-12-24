using System;
using System.Diagnostics;

namespace InGen;

[Conditional("DEBUG")]
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = true)]
public class ScopedAttribute : RegisterAttribute
{
    public ScopedAttribute(Type implementationType, params Type[] registrationTypes) 
        : base(LifeTime.Scoped, implementationType, registrationTypes)
    {
    }
}