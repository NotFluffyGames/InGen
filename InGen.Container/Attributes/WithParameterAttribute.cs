using System;
using System.Diagnostics;

namespace InGen;

[Conditional("DEBUG")]
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = true)]
public class WithParameterAttribute : Attribute
{
    public bool Single { get; }
    public Type ImplementationType { get; }
    public Type[] RegistrationTypes { get; }

    public ExtraRegistrations ExtraRegistrations { get; set; }
    public string? Name { get; set; }
    public string? SourceMember { get; set; }
    
    public WithParameterAttribute(bool single, Type implementationType, params Type[] registrationTypes)
    {
        Single = single;
        ImplementationType = implementationType;
        RegistrationTypes = registrationTypes;
    }

}