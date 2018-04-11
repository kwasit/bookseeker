using Autofac;
using BookSeeker.Providers.Common;

namespace BookSeeker.Provider.Foo
{
    public class FooProviderModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
#if DEBUG
            builder.RegisterType<FooProvider>().As<IBookDataProvider>().InstancePerLifetimeScope();
#endif
        }
    }
}