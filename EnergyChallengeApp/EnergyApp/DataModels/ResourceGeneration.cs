using System.Xml.Serialization;

namespace EnergyApp.DataModels
{
    public class ResourceGeneration
    {
        [XmlElement]
        public string? Name { get; set; }

        [XmlElement]
        public DailyGeneration? DailyGeneration { get; set; }
    }
}
