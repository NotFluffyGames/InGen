namespace InGen.Target;

[InGenContainer]
[Single(typeof(FooInChild))]
[Scoped(typeof(FooScopedInChild))]
public partial class ChildContainer : Container
{
}