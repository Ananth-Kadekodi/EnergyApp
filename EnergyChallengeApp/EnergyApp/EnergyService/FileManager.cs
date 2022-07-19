using EnergyApp.DataModels;
using Microsoft.Extensions.Configuration;
using System.Xml.Serialization;

namespace EnergyApp.EnergyService
{
    public class FileManager
    {
        string referenceDataFilePath = "REFERENCE_FILE_PATH";
        string inputDataFilePath = "INPUT_FILE_PATH";

        public ReferenceData LoadReferenceDataFile()
        {
            ReferenceData referenceData = new ReferenceData();
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ReferenceData));

            var referenceFilePath = retrieveFilePath(referenceDataFilePath);

            using (Stream reader = new FileStream(referenceFilePath, FileMode.Open))
            {
                referenceData = (ReferenceData)xmlSerializer.Deserialize(reader);
            }

            return referenceData;
        }

        public string retrieveFilePath(string filePath)
        {
            var referenceDataConfig = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            return referenceDataConfig.GetValue<string>("AppSettings:"+ filePath);
        }

        public GenerationData LoadInputXMLFile()
        {
            GenerationData generationData = new GenerationData();
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(GenerationData));

            var inputFilePath = retrieveFilePath(inputDataFilePath);

            using (Stream reader = new FileStream(inputFilePath, FileMode.Open))
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
