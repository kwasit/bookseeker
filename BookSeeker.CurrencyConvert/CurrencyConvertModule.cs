using Autofac;

namespace BookSeeker.CurrencyConvert
{
    public class CurrencyConvertModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<FixerCurrencyConvertClient>().As<ICurrencyConvertClient>().InstancePerLifetimeScope();
        }
    }
}