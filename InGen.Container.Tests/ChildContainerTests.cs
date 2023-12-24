using InGen.Target;

namespace InGen.Tests;

[TestFixture]
public class ChildContainerTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void TryResolve_TryResolveFieldInChild_ResolvesSuccessfully()
    {
        var container = new ChildContainer();
        
        Assert.Multiple(() =>
        {
            Assert.That(container.TryResolve<FooInChild>().TryGetValue(out var result), Is.True);
            Assert.That(result, Is.InstanceOf<FooInChild>());
        });
    }
    
    [Test]
    public void TryResolve_TryResolveScopedFieldInChild_ResolvesSuccessfully()
    {
        var container = new ChildContainer();
        
        Assert.Multiple(() =>
        {
            Assert.That(container.TryResolve<FooScopedInChild>().TryGetValue(out var result), Is.True);
            Assert.That(result, Is.InstanceOf<FooScopedInChild>());
        });
    }
}