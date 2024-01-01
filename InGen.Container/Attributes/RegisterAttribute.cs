using System;
using System.Diagnostics;

namespace InGen;

[Conditional("DEBUG")]
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = true)]
public class RegisterAttribute : Attribute
{
    public LifeTime Lifetime { get; }
    public Type ImplementationType { get; }
    public Type[] RegistrationTypes { get; }

    public ExtraRegistrations ExtraRegistrations { get; set; }
    public object? Id { get; set; }
    /// <summary>
    /// Supported sources include: Fields, Properties, Methods (with resolved parameters), Delegates and Lazy
    /// </summary>
    public string? SourceMember { get; set; }
    
    //Todo: think
    public Type[]? WhenInjectedInto { get; set; }

    public RegisterAttribute(LifeTime lifetime, Type implementationType, params Type[] registrationTypes)
    {
        Lifetime = lifetime;
        ImplementationType = implementationType;
        RegistrationTypes = registrationTypes;
    }
}