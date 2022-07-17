using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EnergyApp.DataModels
{
    [XmlRoot(IsNullable = false)]
    public class ReferenceData
    {
        [XmlElement]
        public ReferenceDataFactors Factors { get; set; } = default!;
    }
}
