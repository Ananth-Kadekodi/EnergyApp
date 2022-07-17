using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EnergyApp.DataModels
{
    public class ReferenceDataFactors
    {
        [XmlElement]
        public ReferenceDataFactor EmissionsFactor { get; set; } = default!;

        [XmlElement]
        public ReferenceDataFactor ValueFactor { get; set; } = default!;
    }
}
