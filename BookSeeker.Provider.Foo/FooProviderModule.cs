using Autofac;
using BookSeeker.Providers.Common;

namespace BookSeeker.Provider.Foo
{
    public class FooProviderModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<FooProvider>().As<IBookDataProvider>().InstancePerLifetimeScope();
        }
    }
}