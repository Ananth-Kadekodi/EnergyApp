using System.Xml.Serialization;

namespace EnergyApp.DataModels
{
    public class MaxEmissionGeneratorsData
    {
        [XmlElement("Day")]
        public List<DailyEmissionGenerated>? GeneratorDayEmissions { get; set; }
    }
}
