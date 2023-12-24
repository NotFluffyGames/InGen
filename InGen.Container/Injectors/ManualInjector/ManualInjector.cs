namespace InGen.Container.Injectors
{
    public class ManualInjector : IInjector
    {
        public void Inject(object toInject, IContainer container)
        {
            if (toInject is IInjectable injectable)
                injectable.Inject(container);
        }
    }
}