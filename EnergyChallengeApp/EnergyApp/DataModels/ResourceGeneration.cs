using System.Xml.Serialization;

namespace EnergyApp.DataModels
{
    public class ResourceGeneration
    {
        [XmlElement]
        public string Name { get; set; } = default!;

        [XmlElement]
        public DailyGeneration Generation { get; set; } = default!;
    }
}
