using System;
using System.Diagnostics;

namespace InGen.Container;

[Conditional("DEBUG")]
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = true)]
public class WithParameterAttribute : Attribute
{
    public LifeTime Lifetime { get; }
    public Type ImplementationType { get; }
    public Type[] RegistrationTypes { get; }

    public ExtraRegistrations ExtraRegistrations { get; set; }
    public string? Name { get; set; }
    public string? SourceMember { get; set; }
        
    public WithParameterAttribute(LifeTime lifetime, Type implementationType, params Type[] registrationTypes)
    {
        Lifetime = lifetime;
        ImplementationType = implementationType;
        RegistrationTypes = registrationTypes;
    }

}