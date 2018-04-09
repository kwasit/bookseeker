using Autofac;
using BookSeeker.Common.Extensions;
using BookSeeker.Providers;

namespace BookSeeker.Engine
{
    public class EngineModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule<ProvidersModule>();
            builder.RegisterServices();
        }
    }
}