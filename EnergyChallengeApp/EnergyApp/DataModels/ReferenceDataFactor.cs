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
        public double Low { get; set; }

        [XmlElement]
        public double High { get; set; }

        [XmlElement]
        public double Medium { get; set; }

        
    }
}
