﻿using EnergyApp.DataModels;
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
            var heartRates = new List<GeneratorHeartRates>();

            CalculateOutputs(generationData, referenceData, totalGenerations, highestDailyEmissions, heartRates);
        }


        private void CalculateOutputs(GenerationData generationData, ReferenceData referenceData,  List<Generator> totalGenerations, List<DailyEmissionGenerated> highestDailyEmissions, List<GeneratorHeartRates> heartRates)
        {
            EnergyServiceCalculator energyServiceCalculator = new EnergyServiceCalculator();

            if (generationData.Wind.WindGenerator.Count > 0)
            {
                foreach(var generator in generationData.Wind.WindGenerator)
                {
                    if (generator.Location.Equals("ONSHORE"))
                    {
                        var totalGeneration = energyServiceCalculator.CalculateTotalGeneratorEnergy(generator, referenceData.Factors.ValueFactor.High);
                        totalGenerations.Add(totalGeneration);
                    }

                    if (generator.Location.Equals("OFFSHORE"))
                    {
                        var totalGeneration = energyServiceCalculator.CalculateTotalGeneratorEnergy(generator, referenceData.Factors.ValueFactor.Low);
                        totalGenerations.Add(totalGeneration);
                    }

                }
            }

        }
    }
}
