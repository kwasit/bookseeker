using Autofac;
using BookSeeker.Providers.Common;


namespace BookSeeker.Provider.Amazon
{
    public class AmazonProviderModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AmazonProvider>().As<IBookDataProvider>().InstancePerLifetimeScope();
        }
    }
}