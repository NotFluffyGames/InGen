namespace InGen.Target;

[Single(typeof(ClassWithDependency))]

[Single(typeof(DependentClass), typeof(IDependentInterface))]

//This is meaningless should we throw a warning?
// [Single(typeof(ClassWithDependency), Id = "single-with-none-single-param")]
// [WithParameter(false, typeof(DependentClass), typeof(IDependentInterface))]

//This is meaningless should we throw an warning?
// [Single(typeof(ClassWithDependency), Id = "single-with-single-param")]
// [WithParameter(true, typeof(DependentClass), typeof(IDependentInterface))]

[Scoped(typeof(ClassWithDependency), Id = "scoped-with-none-single-param")]
[WithParameter(false, typeof(DependentClass), typeof(IDependentInterface))]

[Scoped(typeof(ClassWithDependency), Id = "scoped-with-single-param")]
[WithParameter(true, typeof(DependentClass), typeof(IDependentInterface))]

[Transient(typeof(ClassWithDependency), Id = "transient-with-none-single-param")]
[WithParameter(false, typeof(DependentClass), typeof(IDependentInterface))]

[Transient(typeof(ClassWithDependency), Id = "transient-with-single-param")]
[WithParameter(true, typeof(DependentClass), typeof(IDependentInterface))]

public partial class ContainerWithParameters
{
    
}