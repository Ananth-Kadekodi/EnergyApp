using Autofac.Extensions.DependencyInjection;
using EnergyChallengeApp.API;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using EnergyApp.EnergyService;

namespace EnergyChallengeApp.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            /*using (var host = CreateHostBuilder(args).Build())
            {
                await host.StartAsync();
                var lifetime = host.Services.GetRequiredService<IHostApplicationLifetime>();

                EnergyAppService energyAppService = new EnergyAppService();
                energyAppService.RunApplication();

                lifetime.StopApplication();
                await host.WaitForShutdownAsync();
            }*/
            var referenceDataConfig = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            var referenceFilePath = referenceDataConfig.GetValue<string>("AppSettings:FILE_WATCHER_INPUT_PATH");
            EnergyAppService energyAppService = new EnergyAppService();
            energyAppService.MonitorDirectory(referenceFilePath);
            Console.ReadKey();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args);
                
    }
}