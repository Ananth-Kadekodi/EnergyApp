using EnergyApp.DataModels;

namespace EnergyApp.EnergyService
{
    public class ReportProcessorService : IReportProcessorService
    {
        public GenerationOutput ProcessInputReport(GenerationData generationData, ReferenceData referenceData)
        {
            var totalGenerations = new List<Generator>();
            var highestDailyEmissions = new List<DailyEmissionGenerated>();
            var heatRates = new List<GeneratorHeatRates>();

            CalculateOutputs(generationData, referenceData, totalGenerations, highestDailyEmissions, heatRates);
            var generationOutput = new GenerationOutput
            {
                Totals = new GeneratorTotalData { GeneratorTotalValues = totalGenerations},
                MaxEmissionGeneratorsData = new MaxEmissionGeneratorsData { GeneratorDayEmissions = highestDailyEmissions},
                ActualHeatRatesData = new ActualHeatRatesData { GeneratorActualHeatRates = heatRates }
            };

            return generationOutput;
        }


        private void CalculateOutputs(GenerationData generationData, ReferenceData referenceData,  List<Generator> totalGenerations, List<DailyEmissionGenerated> highestDailyEmissions, List<GeneratorHeatRates> heatRates)
        {
            EnergyServiceCalculator energyServiceCalculator = new EnergyServiceCalculator();

            try
            {
                if (generationData.Wind.WindGenerator.Count > 0)
                {
                    foreach (var generator in generationData.Wind.WindGenerator)
                    {
                        if (generator.Location.ToUpper().Equals("ONSHORE"))
                        {
                            var totalGeneration = energyServiceCalculator.CalculateTotalGeneratorEnergy(generator, referenceData.Factors.ValueFactor.High);
                            totalGenerations.Add(totalGeneration);
                        }

                        if (generator.Location.ToUpper().Equals("OFFSHORE"))
                        {
                            var totalGeneration = energyServiceCalculator.CalculateTotalGeneratorEnergy(generator, referenceData.Factors.ValueFactor.Low);
                            totalGenerations.Add(totalGeneration);
                        }
                    }
                }
            } catch (Exception ex)
            {
                Console.WriteLine("Error performing calculations for wind data", ex.Message);
            }

            try
            {
                if (generationData.Coal.CoalGeneratorData.Count > 0)
                {
                    foreach (var generator in generationData.Coal.CoalGeneratorData)
                    {
                        var totalGeneration = energyServiceCalculator.CalculateTotalGeneratorEnergy(generator, referenceData.Factors.ValueFactor.Medium);
                        totalGenerations.Add(totalGeneration);

                        energyServiceCalculator.CalculateMaxGeneratorEmissions(generator, generator.EmissionsRating, highestDailyEmissions, referenceData.Factors.EmissionsFactor.High);

                        var generatorHeatRate = energyServiceCalculator.CalculateHeatRate(generator, generator.ActualNetGeneration, generator.TotalHeatInput);
                        heatRates.Add(generatorHeatRate);

                    }
                }
            } catch (Exception ex)
            {
                Console.WriteLine("Error performing calculations for coal data", ex.Message);
            }
            try
            {
                if (generationData.Gas.GasGeneratorData.Count > 0)
                {
                    foreach (var generator in generationData.Gas.GasGeneratorData)
                    {
                        var totalGeneration = energyServiceCalculator.CalculateTotalGeneratorEnergy(generator, referenceData.Factors.ValueFactor.Medium);
                        totalGenerations.Add(totalGeneration);

                        energyServiceCalculator.CalculateMaxGeneratorEmissions(generator, generator.EmissionsRating, highestDailyEmissions, referenceData.Factors.EmissionsFactor.Medium);
                    }
                }
            } catch (Exception ex)
            {
                Console.WriteLine("Error performing calculations for gas data", ex.Message);
            }
           
        }
    }
}
