namespace InGen.Injector
{
    public interface IInjector
    {
        void Inject(object toInject, IContainer container);
    }
}