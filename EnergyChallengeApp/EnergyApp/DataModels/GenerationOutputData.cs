﻿using System.Xml.Serialization;

namespace EnergyApp.DataModels
{
    public class GenerationOutput
    {
        [XmlElement]
        public GeneratorTotalData? Totals { get; set; }

        [XmlElement("MaxEmissionGenerators")]
        public MaxEmissionGeneratorsData? MaxEmissionGeneratorsData { get; set; }

        [XmlElement("ActualHeatRates")]
        public ActualHeatRatesData? ActualHeatRatesData { get; set; }
    }
}
