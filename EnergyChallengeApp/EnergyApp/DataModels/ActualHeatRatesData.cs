using System.Xml.Serialization;

namespace EnergyApp.DataModels
{
    public class ActualHeatRatesData
    {
        [XmlElement("ActualHeatRate")]
        public List<GeneratorHeatRates>? GeneratorActualHeatRates { get; set; }
    }
}
