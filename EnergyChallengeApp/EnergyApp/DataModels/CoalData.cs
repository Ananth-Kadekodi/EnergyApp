
using System.Xml.Serialization;

namespace EnergyApp.DataModels
{
    public class CoalData
    {
        [XmlElement("CoalGenerator")]
        public List<CoalGeneratorData>? CoalGeneratorData { get; set; }
    }
}
