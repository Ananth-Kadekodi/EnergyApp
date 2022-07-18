using EnergyApp.DataModels;

namespace EnergyApp.EnergyService
{
    public interface IReportProcessorService
    {
        GenerationOutputData ProcessInputReport(GenerationData generationData, ReferenceData referenceData);
    }
}