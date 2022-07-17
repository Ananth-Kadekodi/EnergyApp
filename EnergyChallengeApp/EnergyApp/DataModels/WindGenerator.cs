
using System.Xml.Serialization;

namespace EnergyApp.DataModels
{
    public class WindGenerator : ResourceGeneration
    {
        [XmlElement]
        public string? GeneratorLocation { get; set; }
    }
}
