

using EnergyApp.DataModels;
using System.Linq;

namespace EnergyApp.EnergyService
{
    public class EnergyServiceCalculator
    {
        public Generator CalculateTotalGeneratorEnergy(ResourceGeneration resourceGeneration, double generatorFactor)
        {
            var generator = new Generator { Name = resourceGeneration.Name };
            generator.Total = AddDailyEnergy(resourceGeneration.Generation.DayGenerations, generatorFactor);
            return generator;
        }

        public double AddDailyEnergy(List<DayGeneration> dayGenerations, double generatorFactor)
        {
            double total = 0;

            if (dayGenerations.Count == 0)
            {
                return 0;
            }

            foreach (var dayGeneration in dayGenerations)
            {
                total += CalculateDailyEnergyGenerated(dayGeneration.Energy, dayGeneration.Price, generatorFactor);
            }

            return total;
        }

        public double CalculateDailyEnergyGenerated(double energy, double price, double generatorFactor)
        {
            return (energy * price * generatorFactor);
        }

        public void CalculateMaxGeneratorEmissions(ResourceGeneration generator, double emissionsRating, List<DailyEmissionGenerated> highestDailyEmissions, double emissionFactor)
        {
            CalculateMaxDailyEmissions(generator, emissionsRating, highestDailyEmissions, emissionFactor);
        }

        private void CalculateMaxDailyEmissions(ResourceGeneration generator, double emissionsRating, List<DailyEmissionGenerated> highestDailyEmissions, double emissionFactor)
        {
            foreach (var dayGeneration in generator.Generation.DayGenerations)
            {
                var dayEnergyEmission = CalculateDayEnergyEmission(dayGeneration.Energy, emissionsRating, emissionFactor);

                if (!highestDailyEmissions.Any(s => s.Date == dayGeneration.Date)) {
                   
                    //Add Value if not emission for that date exists
                    addMaxDayEmissionRecord(generator.Name, dayGeneration.Date, dayEnergyEmission, highestDailyEmissions);

                } else if (highestDailyEmissions.Any(s => s.Date == dayGeneration.Date && s.Emission < dayEnergyEmission))
                {
                    var emissionToRemove = highestDailyEmissions.Single(r => r.Date == dayGeneration.Date);
                    
                    highestDailyEmissions.Remove(emissionToRemove);
                    
                    addMaxDayEmissionRecord(generator.Name, dayGeneration.Date, dayEnergyEmission, highestDailyEmissions);
                }
            }
        }

        private void addMaxDayEmissionRecord(string name, DateTime date, double dayEnergyEmission, List<DailyEmissionGenerated> highestDailyEmissions)
        {
            highestDailyEmissions.Add(new DailyEmissionGenerated
            {
                Name = name,
                Date = date,
                Emission = dayEnergyEmission
            });
        }

        private double CalculateDayEnergyEmission(double energy, double emissionsRating, double emissionFactor)
        {
            return energy * emissionsRating * emissionFactor;
        }

        public GeneratorHeatRates CalculateHeatRate(CoalGeneratorData generator, double actualNetGeneration, double totalHeatInput)
        {
            var heatRate = new GeneratorHeatRates{ Name = generator.Name};
            heatRate.HeatRate = RetrieveHeatRate(actualNetGeneration, totalHeatInput);
            return heatRate;
        }

        private double? RetrieveHeatRate(double actualNetGeneration, double totalHeatInput)
        {
            if (actualNetGeneration != 0  && totalHeatInput != 0)
            {
                return totalHeatInput / actualNetGeneration;
            }
            else
            {
                return null;
            }         
        }
    }
}
