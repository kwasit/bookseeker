using Autofac;
using System.Reflection;

namespace BookSeeker.Common.Extensions
{
    public static class ContainerBuilderExtensions
    {
        public static void RegisterServices(this ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Assembly.GetCallingAssembly())
                .Where(t => t.Name.EndsWith("Service"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }
    }
}