using System;

namespace InGen.Target;

[InGenContainer]
[Transient(typeof(ClassWithDependency), typeof(IDependentInterface))]

//Method
[Transient(typeof(Foo), SourceMember = nameof(GetFooMethod), Id = "method")]
[Transient(typeof(IFoo), SourceMember = nameof(GetIFooMethod), Id = "method")]
[Transient(typeof(ClassWithDependency), SourceMember = nameof(GetClassWithDependencyMethod), Id = "method")]

//Field
//Should we throw an error when a field is registered as none single?
//but if it is registered as single we shouldn't save a local field
[Transient(typeof(Foo), SourceMember = nameof(_getFooField), Id = "field")]
[Transient(typeof(IFoo), SourceMember = nameof(_getIFooField), Id = "field")]

//Property
//Should we throw an error when a auto-property is registered as none single?
//but if it is an auto-property and registered as single we shouldn't save a local field
[Transient(typeof(Foo), SourceMember = nameof(GetFooProperty), Id = "property")]
[Transient(typeof(IFoo), SourceMember = nameof(GetIFooProperty), Id = "property")]

//Delegate
[Transient(typeof(Foo), SourceMember = nameof(_getFooDelegate), Id = "delegate")]
[Transient(typeof(IFoo), SourceMember = nameof(_getIFooDelegate), Id = "delegate")]
[Transient(typeof(ClassWithDependency), SourceMember = nameof(_getClassWithDependencyDelegate), Id = "delegate")]

//Lazy<T>
//Should we throw an error when a lazy field is registered as none single?
[Transient(typeof(Foo), SourceMember = nameof(_getFooLazy), Id = "lazy")]
[Transient(typeof(IFoo), SourceMember = nameof(_getIFooLazy), Id = "lazy")]
public partial class ContainerWithSources
{
    private delegate Foo GetFoo();

    private delegate IFoo GetIFoo();

    private delegate ClassWithDependency GetClassWithDependency(IDependentInterface dependentInterface);

    private Foo GetFooMethod() => new Foo();
    private IFoo GetIFooMethod() => GetFooMethod();
    private ClassWithDependency GetClassWithDependencyMethod(IDependentInterface dependentInterface) => new ClassWithDependency(dependentInterface);

    private readonly Foo _getFooField = new Foo();
    private readonly IFoo _getIFooField = new Foo();

    private Foo GetFooProperty { get; } = new Foo();
    private IFoo GetIFooProperty { get; } = new Foo();

    private GetFoo _getFooDelegate;
    private GetIFoo _getIFooDelegate;
    private GetClassWithDependency _getClassWithDependencyDelegate;

    private Lazy<Foo> _getFooLazy;
    private Lazy<IFoo> _getIFooLazy;

    public ContainerWithSources()
    {
        _getFooDelegate = GetFooMethod;
        _getIFooDelegate = GetIFooMethod;
        _getClassWithDependencyDelegate = GetClassWithDependencyMethod;

        _getFooLazy = new(GetFooMethod);
        _getIFooLazy = new(GetIFooMethod);
    }
}