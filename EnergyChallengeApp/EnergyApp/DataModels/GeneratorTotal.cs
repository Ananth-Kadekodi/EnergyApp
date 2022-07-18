using System.Xml.Serialization;

namespace EnergyApp.DataModels
{
    public class GeneratorTotal
    {
        [XmlElement]
        public List<Generator>? Generator{ get; set; }
    }
}
