namespace InGen.Target;

//Todo: add WithParameter to a registration with id
[InGenContainer]
[Single(typeof(SingleClass))]
[Single(typeof(SingleClass), Id = "string_id")]
[Scoped(typeof(ScopedClass))]
[Transient(typeof(TransientClass))]
[Single(typeof(WithIdOnlyClass), Id = 5)]
[Single(typeof(ClassWithDependency))]
[WithParameter(true, typeof(DependentClass), typeof(IDependentInterface))]
[Single(typeof(FooStruct), ExtraRegistrations = ExtraRegistrations.Self | ExtraRegistrations.Interfaces)]
public partial class Container
{
}