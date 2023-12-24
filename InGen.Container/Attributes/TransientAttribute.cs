using System;
using System.Diagnostics;

namespace InGen;

[Conditional("DEBUG")]
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = true)]
public class TransientAttribute : RegisterAttribute
{
    public TransientAttribute(Type implementationType, params Type[] registrationTypes) 
        : base(LifeTime.Transient, implementationType, registrationTypes)
    {
    }
}