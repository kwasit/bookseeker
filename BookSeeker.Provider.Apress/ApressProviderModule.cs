using Autofac;
using BookSeeker.Providers.Common;

namespace BookSeeker.Provider.Apress
{
    public class ApressProviderModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ApressProvider>().As<IBookOffersDataProvider>().InstancePerLifetimeScope();
        }
    }
}