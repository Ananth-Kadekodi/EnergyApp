using EnergyApp.DataModels;
using Microsoft.Extensions.Configuration;
using System.Xml.Serialization;

namespace EnergyApp.EnergyService
{
    public class FileManager
    {
        static string referenceDataFilePath = "REFERENCE_FILE_PATH";
        static string inputDataFilePath = "INPUT_FILE_PATH";
        static string outputDataFilePath = "OUTPUT_FILE_PATH";

        public ReferenceData LoadReferenceDataFile()
        {
            ReferenceData referenceData = new ReferenceData();
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ReferenceData));

            var referenceFilePath = retrieveFilePath(referenceDataFilePath);

            try
            {
                using (Stream reader = new FileStream(referenceFilePath, FileMode.Open))
                {
                    referenceData = (ReferenceData)xmlSerializer.Deserialize(reader);
                }
            } catch (Exception ex)
            {
                Console.WriteLine("Exception retrieving reference file data", ex.Message);
            }
            
            return referenceData;
        }

        private string retrieveFilePath(string filePath)
        {
            var referenceDataConfig = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            return referenceDataConfig.GetValue<string>("AppSettings:"+ filePath);
        }

        public GenerationData LoadInputXMLFile()
        {
            GenerationData generationData = new GenerationData();
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(GenerationData));

            var inputFilePath = retrieveFilePath(inputDataFilePath);

            try
            {
                using (Stream reader = new FileStream(inputFilePath, FileMode.Open))
                {
                    generationData = (GenerationData)xmlSerializer.Deserialize(reader);
                }
            } catch (Exception ex)
            {
                Console.WriteLine("Exception retrieving input data file", ex.Message);
            }

            return generationData;
        }

        public void WriteOutputToFile(GenerationOutput generationOutputData)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(GenerationOutput));
            var outputFilePath = retrieveFilePath(outputDataFilePath);
            var inputFilePath = retrieveFilePath(inputDataFilePath);
            try
            {
                TextWriter writer = new StreamWriter(outputFilePath);
                xmlSerializer.Serialize(writer, generationOutputData);
                writer.Close();

            } catch (Exception ex)
            {
                Console.WriteLine("Error outputting to file", ex.Message);
            }
        }
    }
}
