using System.Xml.Serialization;

namespace EnergyApp.DataModels
{
    public class WindData
    {
        [XmlElement("WindGenerator")]
        public List<WindGenerator> WindGenerator { get; set; } = default!;
    }
}
