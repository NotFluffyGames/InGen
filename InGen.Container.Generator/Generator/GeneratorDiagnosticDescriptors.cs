using static InGen.Container.Generator.DiagnosticDescriptors;

namespace InGen.Container.Generator;

internal static class GeneratorDiagnosticDescriptors
{
    public static readonly DiagnosticDescriptor ServiceRequiredToConstructNotRegistered = Create(
        2,
        "The service registration not found",
        "The service '{0}' required to construct '{1}' is not registered", 
        DiagnosticSeverity.Error);

    public static readonly DiagnosticDescriptor MemberReferencedByInstanceOrFactoryAttributeNotFound = new(
        "JAB0003",
        "A member referenced by instance or factory attribute not found",
        "Unable to find a member '{0}', referenced by '{1}' attribute parameter", 
        "Usage", 
        DiagnosticSeverity.Error, 
        true);

    public static readonly DiagnosticDescriptor MemberReferencedByInstanceOrFactoryAttributeAmbiguous = new("JAB0004",
        "Found multiple members referenced by instance or factory attribute",
        "Found multiple members with the '{0}' name, referenced in the '{1}' attribute parameter", "Usage", DiagnosticSeverity.Error, true);

    public static readonly DiagnosticDescriptor ServiceProviderTypeHasToBePartial = new("JAB0005",
        "The type marked with the ServiceProvider attribute has to be marked partial",
        "The type '{0}' marked with the ServiceProvider attribute has to be marked partial", "Usage", DiagnosticSeverity.Error, true);

    public static readonly DiagnosticDescriptor ImportedTypeNotMarkedWithModuleAttribute = new("JAB0006",
        "The imported type has to be marked with the ServiceProviderModule attribute",
        "The imported type '{0}' has to be marked with the '{1}'", "Usage", DiagnosticSeverity.Error, true);

    public static readonly DiagnosticDescriptor ImplementationTypeRequiresPublicConstructor = new("JAB0007",
        "The implementation type is required to have at least one public constructor",
        "The implementation type '{0}' is required to have at least one public constructor", "Usage", DiagnosticSeverity.Error, true);

    public static readonly DiagnosticDescriptor CyclicDependencyDetected = new("JAB0008",
        "A cyclic dependency detected when resolving a service",
        "A cyclic dependency detected when resolving a service '{0}', dependency chain: '{1}'", "Usage", DiagnosticSeverity.Error, true);

    public static readonly DiagnosticDescriptor MissingServiceProviderAttribute = new("JAB0009",
        "A type contains service registrations but no ServiceProvider or ServiceProviderModule attribute",
        "The type '{0}' contains service registration but is missing ServiceProvider or ServiceProviderModule attribute", "Usage", DiagnosticSeverity.Error, true);

    public static readonly DiagnosticDescriptor NoServiceTypeRegistered = new("JAB0010",
        "The service registration not found",
        "The service type '{0}' is not registered", "Usage", DiagnosticSeverity.Error, true);

    public static readonly DiagnosticDescriptor ImplementationTypeAndFactoryNotAllowed = new("JAB0011",
        "Can't specify both the implementation type and factory/instance",
        "Can't specify both the implementation type and factory/instance, for service '{0}'", "Usage", DiagnosticSeverity.Error, true);

    public static readonly DiagnosticDescriptor FactoryMemberMustBeAMethodOrHaveDelegateType = new("JAB0012",
        "The factory member has to be a method or have a delegate type",
        "The factory member '{0}' has to be a method of have a delegate type, for service '{1}'", "Usage", DiagnosticSeverity.Error, true);

    public static readonly DiagnosticDescriptor ServiceNameMustBeAlphanumeric = new("JAB0015",
        "Service name must be alphanumeric",
        "Service name '{0}' must be non-empty, alphanumeric and start with a letter.", "Usage", DiagnosticSeverity.Error, true);

    public static readonly DiagnosticDescriptor BuiltInServicesAreNotNamed = new("JAB0016",
        "Built-in provider services can not be named",
        "Built-in service '{0}' can not be named", "Usage", DiagnosticSeverity.Error, true);

    public static readonly DiagnosticDescriptor ImplicitIEnumerableNotNamed = new("JAB0017",
        "Implicit IEnumerable<> services can not be named",
        "Implicit IEnumerable service '{0}' can not be named", "Usage", DiagnosticSeverity.Error, true);

    public static readonly DiagnosticDescriptor NoServiceTypeAndNameRegistered = new("JAB0018",
        "The service registration not found",
        "The service type '{0}' and name '{1}' is not registered", "Usage", DiagnosticSeverity.Error, true);

    public static readonly DiagnosticDescriptor NamedServiceRequiredToConstructNotRegistered = new("JAB0019",
        "The named service registration not found",
        "The service '{0}' with name '{1}' required to construct '{2}' is not registered", "Usage", DiagnosticSeverity.Error, true);

    public static readonly DiagnosticDescriptor OnlyStringKeysAreSupported = new("JAB0020",
        "Only string service keys are supported",
        "Service key '{0}' is not supported, only string keys are supported", "Usage", DiagnosticSeverity.Error, true);

    public static readonly DiagnosticDescriptor NullableServiceNotRegistered = new("JAB0013",
        "Not registered nullable dependency without a default value",
        "The nullable service '{0}' requested to construct '{1}' is not registered. Add a default value to make the service reference optional", "Usage", DiagnosticSeverity.Error, true);

    public static readonly DiagnosticDescriptor NullableServiceRegistered = new("JAB0014",
        "Nullable dependency without a default value",
        "'{0}' parameter to construct '{1}' will never be null when constructing using a service provider. Add a default value to make the service reference optional", "Usage", DiagnosticSeverity.Info, true);

    public static readonly DiagnosticDescriptor AbstractNotAllow = new(
        "VCON0002",
        "Injectable type must not be abstract/interface",
        "The injectable type of '{0}' is abstract/interface. It is not allowed",
        Category,
        DiagnosticSeverity.Error,
        true);

    public static readonly DiagnosticDescriptor MultipleCtorAttributeNotSupported = new(
        "VCON0003",
        "[Inject] exists in multiple constructors",
        "Multiple [Inject] constructors exists in '{0}'",
        Category,
        DiagnosticSeverity.Error,
        true);

    public static readonly DiagnosticDescriptor MultipleInjectMethodNotSupported = new(
        "VCON0004",
        "[Inject] exists in multiple methods",
        "Multiple [Inject] methods exists in '{0}'",
        Category,
        DiagnosticSeverity.Error,
        true);

    public static readonly DiagnosticDescriptor PrivateNestedNotSupported = new(
        "VCON0005",
        "Private nested type is not support to code generation.",
        "The injectable object '{0}' is a private nested type. It cannot support code generation ",
        Category,
        DiagnosticSeverity.Warning,
        true);

    public static readonly DiagnosticDescriptor PrivateConstructorNotSupported = new(
        "VCON0006",
        "The private constructor is not supported to code generation.",
        "The injectable constructor of '{0}' is private. It cannot support source generator.",
        Category,
        DiagnosticSeverity.Warning,
        true);

    public static readonly DiagnosticDescriptor PrivateFieldNotSupported = new(
        "VCON0007",
        "The private [Inject] field is not supported to code generation.",
        "The [Inject] field '{0}' does not have accessible to set from the same dll. It cannot support to inject by the source generator.",
        Category,
        DiagnosticSeverity.Warning,
        true);

    public static readonly DiagnosticDescriptor PrivatePropertyNotSupported = new(
        "VCON0008",
        "The private [Inject] property is not supported to code generation",
        "The [Inject] '{0}' does not have accessible to set from the same dll. It cannot support to inject by the source generator.",
        Category,
        DiagnosticSeverity.Warning,
        true);

    public static readonly DiagnosticDescriptor PrivateMethodNotSupported = new(
        "VCON0009",
        "The private [Inject] method is not supported to code generation.",
        "The [Inject] '{0}' does not have accessible to call from the same dll. It cannot support inject by the source generator.",
        Category,
        DiagnosticSeverity.Warning,
        true);

    public static readonly DiagnosticDescriptor GenericsNotSupported = new(
        "VCON0010",
        "The [Inject] constructor or method that require generics argument is not supported to code generation.",
        "[Inject] '{0}' needs generic arguments. It cannot inject by the source generator.",
        Category,
        DiagnosticSeverity.Warning,
        true);

    public static readonly DiagnosticDescriptor ReadonlyFieldNotSupported = new(
        "VCON0011",
        "The readonly [Inject] field is not supported to code generation.",
        "The [Inject] field '{0}' does not have accessible to set a readonly member. It cannot support to inject by the source generator.",
        Category,
        DiagnosticSeverity.Warning,
        true);

    public static readonly DiagnosticDescriptor ReadonlyPropertyNotSupported = new(
        "VCON0012",
        "The readonly [Inject] property is not supported to code generation",
        "The [Inject] '{0}' does not have accessible to set a readonly member. It cannot support to inject by the source generator.",
        Category,
        DiagnosticSeverity.Warning,
        true);
}