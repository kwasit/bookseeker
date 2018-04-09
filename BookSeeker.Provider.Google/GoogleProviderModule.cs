using Autofac;
using BookSeeker.Providers.Common;
using Google.Apis.Books.v1;
using Google.Apis.Services;
using Microsoft.Extensions.Configuration;

namespace BookSeeker.Provider.Google
{
    public class GoogleProviderModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<GoogleProvider>().As<IBookDataProvider>().InstancePerLifetimeScope();
            builder.Register(context =>
                {
                    var configuration = context.Resolve<IConfiguration>();
                    var auth = configuration.GetSection("GoogleAuthentication");
                    return new BooksService(new BaseClientService.Initializer
                    {
                        ApplicationName = auth.GetSection("ApplicationName").Value,
                        ApiKey = auth.GetSection("ApiKey").Value,
                    });
                })
                .AsSelf()
                .InstancePerLifetimeScope();
        }
    }
}