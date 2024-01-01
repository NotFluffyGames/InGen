namespace InGen.Target;

[InGenContainer]
[Single(typeof(SingleInChild))]
[Single(typeof(SingleInChild), Id = "string_id")]
[Scoped(typeof(ScopedInChild))]
[Scoped(typeof(ScopedInChild), Id = "string_id")]
[Transient(typeof(TransientInChild))]
[Transient(typeof(TransientInChild), SourceMember = "CreateTransient", Id = "string_id")]
[Single(typeof(WithIdOnlyInChild), Id = 5)]
public partial class ChildContainer : Container
{
    private static TransientInChild CreateTransient() => new TransientInChild();
}