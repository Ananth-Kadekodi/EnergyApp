using EnergyApp.DataModels;

namespace EnergyApp.EnergyService
{
    public interface IEnergyServiceCalculator
    {
        double CalculateDailyEnergyGenerated(double energy, double price, double generatorFactor);
        GeneratorHeatRates CalculateHeatRate(CoalGeneratorData generator, double actualNetGeneration, double totalHeatInput);
        void CalculateMaxGeneratorEmissions(ResourceGeneration generator, double emissionsRating, List<DailyEmissionGenerated> highestDailyEmissions, double emissionFactor);
        Generator CalculateTotalGeneratorEnergy(ResourceGeneration resourceGeneration, double generatorFactor);
    }
}