

using System.Xml.Serialization;

namespace EnergyApp.DataModels
{
    public class DailyGeneration
    {
        [XmlElement("Day")]
        public List<DayGeneration> DayGenerations { get; set; } = default!;
    }
}
