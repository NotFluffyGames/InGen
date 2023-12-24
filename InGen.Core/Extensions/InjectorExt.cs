using JetBrains.Annotations;

namespace InGen.Injector
{
    public static class InjectorExt
    {
        [UsedImplicitly]
        public static void Inject<TContainer>(this TContainer container, object toInject, object? id = null)
            where TContainer : IContainer, IResolver<IInjector>
        {
            container.Resolve(id).Inject(toInject, container);
        }

        [UsedImplicitly]
        public static void Inject(this IInjector injector, object toInject, IContainer serviceProvider)
        {
            injector.Inject(toInject, serviceProvider);
        }
    }
}