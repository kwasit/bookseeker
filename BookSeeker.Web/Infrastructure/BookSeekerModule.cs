using Autofac;
using BookSeeker.Engine;

namespace BookSeeker.Web.Infrastructure
{
    public class BookSeekerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule<EngineModule>();
        }
    }
}