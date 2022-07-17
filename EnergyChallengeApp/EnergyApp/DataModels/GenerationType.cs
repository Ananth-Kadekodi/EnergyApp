using System.Xml.Serialization;

namespace EnergyApp.DataModels
{
    [XmlRoot(ElementName = "GenerationReport", IsNullable = false)]
    public class GenerationData
    {
        [XmlElement]
        public CoalData Coal { get; set; } = default!;

        [XmlElement]
        public WindData Wind { get; set; } = default!;

        [XmlElement]
        public GasData Gas { get; set; } = default!;
    }
}
