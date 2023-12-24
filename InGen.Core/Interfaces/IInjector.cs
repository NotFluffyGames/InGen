namespace InGen.Container.Injectors
{
    public interface IInjector
    {
        void Inject(object toInject, IContainer container);
    }
}