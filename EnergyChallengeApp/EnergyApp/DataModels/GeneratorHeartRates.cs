using System.Xml.Serialization;

namespace EnergyApp.DataModels
{
    public class GeneratorHeartRates
    {
        [XmlElement]
        public string? Name { get; set; }

        [XmlElement]
        public double? HeatRate { get; set; }
    }
}
