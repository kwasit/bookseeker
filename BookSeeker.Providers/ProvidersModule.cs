using Autofac;
using BookSeeker.Provider.Amazon;
using BookSeeker.Provider.Google;

namespace BookSeeker.Providers
{
    public class ProvidersModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule<AmazonProviderModule>();
            builder.RegisterModule<GoogleProviderModule>();
        }
    }
}