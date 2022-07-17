
using System.Xml.Serialization;

namespace EnergyApp.DataModels
{
    public class CoalGeneratorData : ResourceGeneration
    {
        [XmlElement]
        public decimal ActualNetGeneration { get; set; }

        [XmlElement]
        public decimal EmissionsRating { get; set; }

        [XmlElement]
        public decimal TotalHeatInput { get; set; }
      
    }
}
