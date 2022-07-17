using System.Xml.Serialization;

namespace EnergyApp.DataModels
{
    public class DayGeneration
    {
        [XmlElement]
        public decimal Price { get; set; }

        [XmlElement]
        public DateTime Date { get; set; }

        [XmlElement]
        public decimal Energy { get; set; }
    }
}
