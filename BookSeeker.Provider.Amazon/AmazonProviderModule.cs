using Autofac;
using BookSeeker.Providers.Common;
using Microsoft.Extensions.Configuration;
using Nager.AmazonProductAdvertising;


namespace BookSeeker.Provider.Amazon
{
    public class AmazonProviderModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AmazonProvider>().As<IBookDataProvider>().InstancePerLifetimeScope();
            builder.Register(context =>
                {
                    var configuration = context.Resolve<IConfiguration>();
                    var auth = configuration.GetSection("AmazonAuthentication");
                    return new AmazonWrapper(new AmazonAuthentication
                    {
                        AccessKey = auth.GetSection("AccessKey").Value,
                        SecretKey = auth.GetSection("SecretKey").Value
                    }, AmazonEndpoint.US);
                })
                .As<IAmazonWrapper>()
                .InstancePerLifetimeScope();
        }
    }
}