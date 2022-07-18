

using EnergyApp.DataModels;

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

            if(dayGenerations.Count == 0)
            {
                return 0;
            }

            foreach(var dayGeneration in dayGenerations)
            {
                total += CalculateDailyEnergyGenerated(dayGeneration.Energy, dayGeneration.Price, generatorFactor);
            }

            return total;
        }

        public double CalculateDailyEnergyGenerated(double energy, double price, double generatorFactor)
        {
            return (energy * price * generatorFactor);
        }
    }
}
