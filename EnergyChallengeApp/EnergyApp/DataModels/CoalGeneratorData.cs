
using System.Xml.Serialization;

namespace EnergyApp.DataModels
{
    public class CoalGeneratorData : ResourceGeneration
    {
        [XmlElement]
        public double ActualNetGeneration { get; set; }

        [XmlElement]
        public double EmissionsRating { get; set; }

        [XmlElement]
        public double TotalHeatInput { get; set; }
      
    }
}
