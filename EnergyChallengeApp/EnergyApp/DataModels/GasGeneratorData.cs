using System.Xml.Serialization;

namespace EnergyApp.DataModels
{
    public class GasGeneratorData: ResourceGeneration
    {
        [XmlElement]
        public decimal EmissionsRating { get; set; }
    }
}
