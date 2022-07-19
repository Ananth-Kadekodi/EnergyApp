using EnergyApp.EnergyService;

namespace EnergyChallengeApp.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var referenceDataConfig = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            var referenceFilePath = referenceDataConfig.GetValue<string>("AppSettings:FILE_WATCHER_INPUT_PATH");
            
            EnergyAppService energyAppService = new EnergyAppService();
            energyAppService.MonitorDirectory(referenceFilePath);
           
            Console.ReadKey();
        }       
    }
}