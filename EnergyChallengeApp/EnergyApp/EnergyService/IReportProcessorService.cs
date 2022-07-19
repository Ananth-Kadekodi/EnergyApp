using EnergyApp.DataModels;

namespace EnergyApp.EnergyService
{
    public interface IReportProcessorService
    {
        GenerationOutput ProcessInputReport(GenerationData generationData, ReferenceData referenceData);
    }
}