using EnergyApp.DataModels;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Configuration;

namespace EnergyApp.EnergyService
{
    public class FileManager
    {
        public ReferenceData LoadReferenceDataFile()
        {
            ReferenceData referenceData = new ReferenceData();
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ReferenceData));

            var referenceDataConfig = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            var referenceFilePath = referenceDataConfig.GetValue<string>("AppSettings:REFERENCE_FILE_PATH");
            var referenceFileName = referenceDataConfig.GetValue<string>("AppSettings:REFERENCE_FILE_NAME");

            using (Stream reader = new FileStream(referenceFilePath, FileMode.Open))
            {
                referenceData = (ReferenceData)xmlSerializer.Deserialize(reader);
            }

            return referenceData;
        }

        public GenerationData LoadInputXMLFile()
        {
            GenerationData generationData = new GenerationData();
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(GenerationData));
            

            var referenceDataConfig = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            var referenceFilePath = referenceDataConfig.GetValue<string>("AppSettings:INPUT_FILE_PATH");
            var referenceFileName = referenceDataConfig.GetValue<string>("AppSettings:INPUT_FILE_NAME");

            using (Stream reader = new FileStream(referenceFilePath, FileMode.Open))
            {
                generationData = (GenerationData)xmlSerializer.Deserialize(reader);
            }

            return generationData;
        }

        public void WriteOutputToFile(GenerationOutputData generationOutputData)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(GenerationOutputData));
            var outputFileDataConfig = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            var outputFilePath = outputFileDataConfig.GetValue<string>("AppSettings:OUTPUT_FILE_PATH");
            TextWriter writer = new StreamWriter(outputFilePath);
            xmlSerializer.Serialize(writer, generationOutputData);
            writer.Close();
        }
    }
}
