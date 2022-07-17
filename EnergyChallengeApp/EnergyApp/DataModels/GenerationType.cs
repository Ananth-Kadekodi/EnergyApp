using System.Xml.Serialization;

namespace EnergyApp.DataModels
{
    public class GenerationType
    {
        [XmlElement]
        public CoalData Coal { get; set; }

        [XmlElement]
        public WindData WindData { get; set; }

        [XmlElement]
        public GasData Gas { get; set; }

        
    }
}
