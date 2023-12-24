using System;
using System.Diagnostics;

namespace InGen.Container;

[Conditional("DEBUG")]
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = true)]
public class WhenInjectedIntoAttribute : Attribute
{
    public LifeTime Lifetime { get; }
    public Type ImplementationType { get; }
    public Type[] TargetTypes { get; }

    public ExtraRegistrations ExtraRegistrations { get; set; }
    public Type[]? RegistrationTypes { get; set; }
    public string? SourceMember { get; set; }

    public WhenInjectedIntoAttribute(LifeTime lifetime, Type implementationType, params Type[] targetTypes)
    {
        Lifetime = lifetime;
        ImplementationType = implementationType;
        TargetTypes = targetTypes;
    }
}