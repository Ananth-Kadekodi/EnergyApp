namespace EnergyChallengeApp.API.ModuleConfiguration
{
    public interface IService
    {
        ServiceStatus Status { get; set; }
        string Name { get; }

        //void Start();
        //void Stop();
    }

    public enum ServiceStatus
    {
        Started = 0,
        Stopped = 1
    }
}