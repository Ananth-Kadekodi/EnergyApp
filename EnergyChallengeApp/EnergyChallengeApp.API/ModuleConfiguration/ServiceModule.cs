using Autofac;

namespace EnergyChallengeApp.API.ModuleConfiguration
{
    public class ServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            //builder.RegisterType<HarveyDownloadService>().As<IService>().SingleInstance();
        }
    }
}
