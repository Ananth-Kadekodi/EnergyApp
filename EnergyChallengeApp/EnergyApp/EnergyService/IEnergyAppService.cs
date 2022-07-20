namespace EnergyApp.EnergyService
{
    public interface IEnergyAppService
    {
        void MonitorDirectory(string path);
        void RunApplication();
    }
}