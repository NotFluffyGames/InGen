namespace InGen.Tests;

[TestFixture]
public class ParametersTests
{
    [Test]
    public void Resolve_ResolveTypeWithRegisteredDependency_ResolvesSuccessfully()
    {
        var container = new ContainerWithParameters();
        var instance = container.Resolve<ClassWithDependency>();
        
        Assert.Multiple(() =>
        {
            Assert.That(instance, Is.TypeOf<ClassWithDependency>());
            Assert.That(instance.DependentInterface, Is.TypeOf<DependentClass>());
        });
    }
    
    [Test]
    public void Resolve_ResolveScopedRegisterWithNoneSingleParam_ResolvedInstanceAndParameterDifferent()
    {
        var container = new ContainerWithParameters();
        var instance = container.Resolve<ClassWithDependency>("scoped-with-none-single-param");
        var scopedInstance = container.CreateScope().Resolve<ClassWithDependency>("scoped-with-none-single-param");

        Assert.Multiple(() =>
        {
            Assert.That(instance, Is.SameAs(container.Resolve<ClassWithDependency>("scoped-with-none-single-param")));
            Assert.That(instance, Is.Not.SameAs(scopedInstance));
            Assert.That(instance.DependentInterface, Is.Not.SameAs(scopedInstance.DependentInterface));
        });
    }

    [Test]
    public void Resolve_ResolveScopedRegisterWithSingleParam_ResolvedInstanceIsDifferentButParameterInstanceIsShared()
    {
        var container = new ContainerWithParameters();
        var a = container.Resolve<ClassWithDependency>("scoped-with-single-param");
        var b = container.Resolve<ClassWithDependency>("scoped-with-single-param");
        var scopedInstance = container.CreateScope().Resolve<ClassWithDependency>("scoped-with-single-param");

        Assert.Multiple(() =>
        {
            Assert.That(a, Is.SameAs(b));
            Assert.That(a, Is.Not.SameAs(scopedInstance));
            Assert.That(a.DependentInterface, Is.SameAs(scopedInstance.DependentInterface));
        });
    }
    
    [Test]
    public void Resolve_ResolveTransientRegisterWithNoneSingleParam_ResolvedInstanceAndParameterDifferent()
    {
        var container = new ContainerWithParameters();
        var a = container.Resolve<ClassWithDependency>("transient-with-none-single-param");
        var b = container.Resolve<ClassWithDependency>("transient-with-none-single-param");

        Assert.Multiple(() =>
        {
            Assert.That(a, Is.Not.SameAs(b));
            Assert.That(a.DependentInterface, Is.Not.SameAs(b.DependentInterface));
        });
    }

    [Test]
    public void Resolve_ResolveTransientRegisterWithSingleParam_ResolvedInstanceIsDifferentButParameterInstanceIsShared()
    {
        var container = new ContainerWithParameters();
        var a = container.Resolve<ClassWithDependency>("transient-with-single-param");
        var b = container.Resolve<ClassWithDependency>("transient-with-single-param");

        Assert.Multiple(() =>
        {
            Assert.That(a, Is.Not.SameAs(b));
            Assert.That(a.DependentInterface, Is.SameAs(b.DependentInterface));
        });
    }
}