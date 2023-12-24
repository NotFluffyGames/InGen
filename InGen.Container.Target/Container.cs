namespace InGen.Container.Target;

[InGenContainer]
[Single(typeof(SingleClass))]
[Scoped(typeof(ScopedClass))]
[Transient(typeof(TransientClass))]
[Single(typeof(ClassWithDependency))]
[WithParameter(LifeTime.Single, typeof(DependentClass), typeof(IDependentInterface))]
[Single(typeof(FooStruct), ExtraRegistrations = ExtraRegistrations.Self | ExtraRegistrations.Interfaces)]
public partial class Container
{
}

[InGenContainer]
[Single(typeof(FooInChild))]
[Scoped(typeof(FooScopedInChild))]
public partial class ChildContainer : Container
{
}