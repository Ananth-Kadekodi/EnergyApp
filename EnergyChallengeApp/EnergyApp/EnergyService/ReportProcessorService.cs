using EnergyApp.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnergyApp.EnergyService
{
    public class ReportProcessorService : IReportProcessorService
    {
        public void ProcessInputReport(GenerationData generationData, ReferenceData referenceData)
        {
            var totalGenerations = new List<Generator>();
            var highestDailyEmissions = new List<DailyEmissionGenerated>();
            var heatRates = new List<GeneratorHeatRates>();

            CalculateOutputs(generationData, referenceData, totalGenerations, highestDailyEmissions, heatRates);
        }


        private void CalculateOutputs(GenerationData generationData, ReferenceData referenceData,  List<Generator> totalGenerations, List<DailyEmissionGenerated> highestDailyEmissions, List<GeneratorHeatRates> heatRates)
        {
            EnergyServiceCalculator energyServiceCalculator = new EnergyServiceCalculator();

            if (generationData.Wind.WindGenerator.Count > 0)
            {
                foreach(var generator in generationData.Wind.WindGenerator)
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

            if(generationData.Coal.CoalGeneratorData.Count > 0)
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

        }
    }
}
