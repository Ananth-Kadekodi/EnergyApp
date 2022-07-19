using Autofac;
using EnergyApp.EnergyService;

namespace EnergyChallengeApp.API.ModuleConfiguration
{
    public class ServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            //builder.RegisterType<EnergyAppService>().As<IEnergyAppService>().SingleInstance();
        }
    }
}
