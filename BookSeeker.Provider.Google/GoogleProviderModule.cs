using Autofac;
using BookSeeker.Providers.Common;

namespace BookSeeker.Provider.Google
{
    public class GoogleProviderModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<GoogleProvider>().As<IBookSearchDataProvider>().InstancePerLifetimeScope();
            builder.RegisterType<GoogleProvider>().As<IBookOffersDataProvider>().InstancePerLifetimeScope();
        }
    }
}