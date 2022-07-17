using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EnergyApp.DataModels
{
    public class ReferenceDataFactor
    {
        [XmlElement]
        public decimal Low { get; set; }

        [XmlElement]
        public decimal High { get; set; }

        [XmlElement]
        public decimal Medium { get; set; }

        
    }
}
