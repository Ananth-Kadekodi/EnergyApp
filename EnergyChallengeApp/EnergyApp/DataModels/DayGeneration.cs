using System.Xml.Serialization;

namespace EnergyApp.DataModels
{
    public class DayGeneration
    {
        [XmlElement]
        public double Price { get; set; }

        [XmlElement]
        public DateTime Date { get; set; }

        [XmlElement]
        public double Energy { get; set; }
    }
}
