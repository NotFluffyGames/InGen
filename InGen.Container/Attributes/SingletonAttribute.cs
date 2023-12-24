using System;
using System.Diagnostics;

namespace InGen.Container;

[Conditional("DEBUG")]
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = true)]
public class SingleAttribute : RegisterAttribute
{
    public SingleAttribute(Type implementationType, params Type[] registrationTypes) 
        : base(LifeTime.Single, implementationType, registrationTypes)
    {
    }
}