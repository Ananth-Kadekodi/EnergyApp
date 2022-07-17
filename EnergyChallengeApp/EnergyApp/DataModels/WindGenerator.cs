
using System.Xml.Serialization;

namespace EnergyApp.DataModels
{
    public class WindGenerator : ResourceGeneration
    {
        [XmlElement]
        public string? Location { get; set; }
    }
}
