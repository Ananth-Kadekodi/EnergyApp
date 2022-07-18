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
            FileManager fileManager = new FileManager();
            ReportProcessorService reportProcessorService = new ReportProcessorService();

            //CreateHostBuilder(args).Build().Run();
            var referenceData = fileManager.LoadReferenceDataFile();
            var generationReportData = fileManager.LoadInputXMLFile();
            reportProcessorService.ProcessInputReport(generationReportData, referenceData);
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}