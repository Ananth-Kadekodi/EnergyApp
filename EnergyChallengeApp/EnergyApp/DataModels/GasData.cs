using System.Xml.Serialization;

namespace EnergyApp.DataModels
{
    public class GasData
    {
        [XmlElement("GasGenerator")]
        public List<GasGeneratorData>? GasGeneratorData { get; set; }
    }
}
