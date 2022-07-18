using System.Xml.Serialization;

namespace EnergyApp.DataModels
{
    public class DailyEmissionGenerated
    {
        [XmlElement]
        public string? Name { get; set; }

        [XmlElement]
        public DateTime Date { get; set; }

        [XmlElement]
        public double Emission { get; set; }
    }
}
