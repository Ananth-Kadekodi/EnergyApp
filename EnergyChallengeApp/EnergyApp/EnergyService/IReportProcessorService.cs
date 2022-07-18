using EnergyApp.DataModels;

namespace EnergyApp.EnergyService
{
    public interface IReportProcessorService
    {
        void ProcessInputReport(GenerationData generationData, ReferenceData referenceData);
    }
}