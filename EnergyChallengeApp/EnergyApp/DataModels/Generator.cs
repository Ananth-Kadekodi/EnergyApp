using System.Xml.Serialization;

namespace EnergyApp.DataModels
{
    public class Generator
    {
        [XmlElement]
        public string? Name { get; set; }

        [XmlElement]
        public double? Total { get; set; }

    }
}
