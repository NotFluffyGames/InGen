namespace InGen.Container.Injectors
{
    public class CombineInjectors : IInjector
    {
        private readonly IInjector[] _injectors;

        public CombineInjectors(params IInjector[] injectors)
        {
            _injectors = injectors;
        }

        public void Inject(object toInject, IContainer container)
        {
            foreach (var injector in _injectors) 
                injector.Inject(toInject, container);
        }
    }
}
