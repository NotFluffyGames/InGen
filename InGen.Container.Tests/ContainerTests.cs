namespace InGen.Container.Tests;

[TestFixture]
public class ContainerTests
{
    private static IEnumerable<IContainer> AllContainers
    {
        get
        {
            yield return new Target.Container();
            yield return new ChildContainer();
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
                select new object[] { container, supportedType };
        }
    }

    [TestCaseSource(nameof(ContainerTypesWithScopeType))]
    public void CreateScope_CompareScopeSupportedTypes_AllSupportedTypesMatch(Type containerType, Type scopeType)
    {
        CollectionAssert.AreEquivalent(AllSupportedTypes(containerType), AllSupportedTypes(scopeType));
    }

    //Todo: take into account ids
    [TestCaseSource(nameof(AllContainersAndAllTypes))]
    public void Resolve_ResolveSupportedType_ResolvedSuccessfully(IContainer container, Type typeToResolve)
    {
        var result = container.Resolve(typeToResolve);
        Assert.That(result, Is.InstanceOf(typeToResolve));
    }
    
    [TestCaseSource(nameof(AllContainersAndAllTypes))]
    public void TryResolve_ResolveSupportedType_ResolvedSuccessfully(IContainer container, Type typeToResolve)
    {
        var result = container.TryResolve(typeToResolve);
        Assert.That(result.GetValue(), Is.InstanceOf(typeToResolve));
    }


    private static IEnumerable<Type> AllSupportedTypes(IContainer container) => AllSupportedTypes(container.GetType());
    private static IEnumerable<Type> AllSupportedTypes(Type containerType)
    {
        return from inter
                in containerType.GetInterfaces()
                where inter.IsGenericType
                where inter.GetGenericTypeDefinition().IsAssignableTo(typeof(IResolver<>))
                select inter.GenericTypeArguments.First();
    }
}