using System.Xml.Serialization;

namespace EnergyApp.DataModels
{
    public class GeneratorTotalData
    {
        [XmlElement("Generator")]
        public List<Generator> GeneratorTotalValues { get; set;}

    }
}
