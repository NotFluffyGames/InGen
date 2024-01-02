using InGen.Exceptions;

namespace InGen.Tests;

[TestFixture]
public class ContainerTests 
{
    private static readonly object InvalidId = "Invalid_id";

    private static IEnumerable<IContainer> AllContainers
    {
        get
        {
            yield return new Container();
            yield return new ChildContainer();
            yield return new ContainerWithParameters();
            yield return new ContainerWithSources();
        }
    }

    private static IEnumerable<IContainer> AllContainersAndAllScopes 
    {
        get
        {
            IContainer container = new Target.Container();
            yield return container;
            yield return container.CreateScope();
            yield return new Target.Container().CreateScope();

            container = new ChildContainer();
            yield return container;
            yield return container.CreateScope();
            yield return new ChildContainer().CreateScope();

            container = new ContainerWithParameters();
            yield return container;
            yield return container.CreateScope();
            yield return new ContainerWithParameters().CreateScope();

            container = new ContainerWithSources();
            yield return container;
            yield return container.CreateScope();
            yield return new ContainerWithSources().CreateScope();
        }
    }

    private static IEnumerable<object> ContainerTypesWithScopeType
        => AllContainers.Select(container => new object[] { container.GetType(), container.CreateScope().GetType() });

    private static IEnumerable<object> AllContainersAndAllTypes
    {
        get
        {
            return
                from container
                    in AllContainersAndAllScopes
                from supportedType
                    in AllSupportedTypes(container)
                select new object[] { container, supportedType.type, supportedType.id, supportedType.lifeTime };
        }
    }

    [TestCaseSource(nameof(ContainerTypesWithScopeType))]
    public void CreateScope_CompareScopeSupportedTypes_AllSupportedTypesMatch(Type containerType, Type scopeType)
    {
        Assert.Multiple(
            () =>
            {
                CollectionAssert.AreEquivalent(AllSupportedTypesFromInterfaces(containerType), AllSupportedTypesFromInterfaces(scopeType));
                CollectionAssert.AreEquivalent(AllSupportedTypes(containerType), AllSupportedTypes(scopeType));
            });
    }

    [TestCaseSource(nameof(AllContainersAndAllTypes))]
    public void Resolve_ResolveSupportedType_ResolvedSuccessfully(IContainer container, Type typeToResolve, object? id, LifeTime lifeTime)
    {
        var result = container.Resolve(typeToResolve, id);
        Assert.That(result, Is.InstanceOf(typeToResolve));
    }

    [TestCaseSource(nameof(AllContainersAndAllTypes))]
    public void Resolve_ResolveSupportedTypeWithInvalidId_ThrowsInvalidIdException(IContainer container, Type typeToResolve, object? id, LifeTime lifeTime)
    {
        try
        {
            container.Resolve(typeToResolve, InvalidId);
        }
        catch (InvalidIdException invalidIdException)
        {
            Assert.Multiple(() =>
            {
                Assert.That(invalidIdException.ResolveType.IsAssignableTo(typeToResolve));
                Assert.That(invalidIdException.Id, Is.EqualTo(InvalidId));
            });

            return;
        }

        Assert.Fail();
    }

    [TestCaseSource(nameof(AllContainersAndAllTypes))]
    public void Resolve_ResolveSupportedTypeWithLifetime_LifetimeIsCorrect(IContainer container, Type typeToResolve, object? id, LifeTime lifeTime)
    {
        switch (lifeTime)
        {
            case LifeTime.Single:
                Assert.Multiple(() =>
                {
                    var instance = container.Resolve(typeToResolve, id);
                    Assert.That(container.Resolve(typeToResolve, id), Is.EqualTo(instance));
                    Assert.That(container.CreateScope().Resolve(typeToResolve, id), Is.EqualTo(instance));
                });
                break;

            case LifeTime.Scoped:
                Assert.Multiple(() =>
                {
                    var instance = container.Resolve(typeToResolve, id);
                    Assert.That(container.Resolve(typeToResolve, id), Is.EqualTo(instance));

                    var scope = container.CreateScope();
                    var scopeInstance = scope.Resolve(typeToResolve, id);
                    Assert.That(scopeInstance, Is.Not.EqualTo(instance));
                    Assert.That(scope.Resolve(typeToResolve, id), Is.EqualTo(scopeInstance));
                });
                break;

            case LifeTime.Transient:
                var instance = container.Resolve(typeToResolve, id);
                Assert.That(container.Resolve(typeToResolve, id), Is.Not.EqualTo(instance));

                var scope = container.CreateScope();
                var scopeInstance = scope.Resolve(typeToResolve, id);
                Assert.That(scopeInstance, Is.Not.EqualTo(instance));
                Assert.That(scope.Resolve(typeToResolve, id), Is.Not.EqualTo(scopeInstance));
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(lifeTime), lifeTime, null);
        }
    }

    [TestCaseSource(nameof(AllContainersAndAllTypes))]
    public void TryResolve_ResolveSupportedType_ResolvedSuccessfully(IContainer container, Type typeToResolve, object? id, LifeTime lifeTime)
    {
        var result = container.TryResolve(typeToResolve, id);
        Assert.That(result.GetValue(), Is.InstanceOf(typeToResolve));
    }

    [TestCaseSource(nameof(AllContainersAndAllTypes))]
    public void TryResolve_ResolveSupportedTypeWithInvalidId_FailsToResolve(IContainer container, Type typeToResolve, object? id, LifeTime lifeTime)
    {
        var result = container.TryResolve(typeToResolve, InvalidId);

        Assert.Multiple(() =>
        {
            Assert.That(result.Exception, Is.InstanceOf<InvalidIdException>());
            var invalidIdExc = (InvalidIdException)result.Exception!;
            Assert.That(invalidIdExc.ResolveType.IsAssignableTo(typeToResolve));
            Assert.That(invalidIdExc.Id, Is.EqualTo(InvalidId));
        });
    }

    [TestCaseSource(nameof(AllContainersAndAllTypes))]
    public void TryResolve_ResolveSupportedTypeWithLifetime_LifetimeIsCorrect(IContainer container, Type typeToResolve, object? id, LifeTime lifeTime)
    {
        switch (lifeTime)
        {
            case LifeTime.Single:
                Assert.Multiple(() =>
                {
                    var instance = container.TryResolve(typeToResolve, id).GetValue();
                    Assert.That(container.TryResolve(typeToResolve, id).GetValue(), Is.EqualTo(instance));
                    Assert.That(container.CreateScope().TryResolve(typeToResolve, id).GetValue(), Is.EqualTo(instance));
                });
                break;

            case LifeTime.Scoped:
                Assert.Multiple(() =>
                {
                    var instance = container.TryResolve(typeToResolve, id).GetValue();
                    Assert.That(container.TryResolve(typeToResolve, id).GetValue(), Is.EqualTo(instance));

                    var scope = container.CreateScope();
                    var scopeInstance = scope.TryResolve(typeToResolve, id).GetValue();
                    Assert.That(scopeInstance, Is.Not.EqualTo(instance));
                    Assert.That(scope.TryResolve(typeToResolve, id).GetValue(), Is.EqualTo(scopeInstance));
                });
                break;

            case LifeTime.Transient:
                var instance = container.TryResolve(typeToResolve, id).GetValue();
                Assert.That(container.TryResolve(typeToResolve, id).GetValue(), Is.Not.EqualTo(instance));

                var scope = container.CreateScope();
                var scopeInstance = scope.TryResolve(typeToResolve, id).GetValue();
                Assert.That(scopeInstance, Is.Not.EqualTo(instance));
                Assert.That(scope.TryResolve(typeToResolve, id).GetValue(), Is.Not.EqualTo(scopeInstance));
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(lifeTime), lifeTime, null);
        }
    }

    [Test]
    public void Resolve_ResolveTypeWithParameter_ResolvesSuccessfullyWithCorrectLifetime()
    {
        var container = new Container();
        
        
    }

    private static IEnumerable<(Type type, object? id, LifeTime lifeTime)> AllSupportedTypes(IContainer container) => AllSupportedTypes(container.GetType());

    private static IEnumerable<(Type type, object? id, LifeTime lifeTime)> AllSupportedTypes(Type containerType)
    {
        if (containerType == typeof(Container) || containerType == typeof(Container.Scope))
        {
            yield return (typeof(FooStruct), null, LifeTime.Single);
            yield return (typeof(IFoo), null, LifeTime.Single);
            yield return (typeof(SingleClass), null, LifeTime.Single);
            yield return (typeof(SingleClass), "string_id", LifeTime.Single);
            yield return (typeof(ScopedClass), null, LifeTime.Scoped);
            yield return (typeof(TransientClass), null, LifeTime.Transient);
            yield return (typeof(ClassWithDependency), null, LifeTime.Single);
            yield return (typeof(ClassWithDependency), "string_id", LifeTime.Single);
            yield return (typeof(WithIdOnlyClass), 5, LifeTime.Single);
        }
        else if (containerType == typeof(ChildContainer) || containerType == typeof(ChildContainer.Scope))
        {
            foreach (var type in AllSupportedTypes(typeof(Container)))
                yield return type;

            yield return (typeof(SingleInChild), null, LifeTime.Single);
            yield return (typeof(ScopedInChild), null, LifeTime.Scoped);
            yield return (typeof(TransientInChild), null, LifeTime.Transient);
            yield return (typeof(WithIdOnlyInChild), 5, LifeTime.Single);
        }
        else if (containerType == typeof(ContainerWithParameters) || containerType == typeof(ContainerWithParameters.Scope))
        {
            yield return (typeof(ClassWithDependency), null, LifeTime.Single);
            yield return (typeof(IDependentInterface), null, LifeTime.Single);
            yield return (typeof(ClassWithDependency), "scoped-with-none-single-param", LifeTime.Scoped);
            yield return (typeof(ClassWithDependency), "scoped-with-single-param", LifeTime.Scoped);
            yield return (typeof(ClassWithDependency), "transient-with-none-single-param", LifeTime.Transient);
            yield return (typeof(ClassWithDependency), "transient-with-single-param", LifeTime.Transient);
        }
        else if(containerType == typeof(ContainerWithSources) || containerType == typeof(ContainerWithSources.Scope))
        {
            yield return (typeof(IDependentInterface), null, LifeTime.Transient);
            
            yield return (typeof(Foo), "method", LifeTime.Transient);
            yield return (typeof(Foo), "field", LifeTime.Single);
            yield return (typeof(Foo), "property", LifeTime.Single);
            yield return (typeof(Foo), "delegate", LifeTime.Transient);
            yield return (typeof(Foo), "lazy", LifeTime.Single);
            
            yield return (typeof(IFoo), "method", LifeTime.Transient);
            yield return (typeof(IFoo), "field", LifeTime.Single);
            yield return (typeof(IFoo), "property", LifeTime.Single);
            yield return (typeof(IFoo), "delegate", LifeTime.Transient);
            yield return (typeof(IFoo), "lazy", LifeTime.Single);
             
            yield return (typeof(ClassWithDependency), "method", LifeTime.Transient);
            yield return (typeof(ClassWithDependency), "delegate", LifeTime.Transient);
        }
    }

    private static IEnumerable<Type> AllSupportedTypesFromInterfaces(Type containerType)
    {
        return from inter
                in containerType.GetInterfaces()
            where inter.IsGenericType
            where inter.GetGenericTypeDefinition().IsAssignableTo(typeof(IResolver<>))
            select inter.GenericTypeArguments.First();
    }
}