using System.Xml.Serialization;

namespace EnergyApp.DataModels
{
    public class GasGeneratorData: ResourceGeneration
    {
        [XmlElement]
        public double EmissionsRating { get; set; }
    }
}
