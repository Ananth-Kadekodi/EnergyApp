using EnergyApp.DataModels;
using Microsoft.Extensions.Configuration;
using System.Xml.Serialization;

namespace EnergyApp.EnergyService
{
    public class FileManager
    {
        private static string referenceDataFilePath = "REFERENCE_FILE_PATH";
        private static string inputDataFilePath = "INPUT_FILE_PATH";
        private static string outputDataFilePath = "OUTPUT_FILE_PATH";
        private static string archiveDataFilePath = "ARCHIVE_FILE_PATH";

        public ReferenceData LoadReferenceDataFile(string fileName)
        {
            ReferenceData referenceData = new ReferenceData();
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ReferenceData));

            var referenceFilePath = RetrieveFilePath(referenceDataFilePath,fileName);

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

        public string RetrieveFilePath(string fileDirectory, string fileName)
        {
            var referenceDataConfig = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            var fileDirectoryPath = referenceDataConfig.GetValue<string>("AppSettings:"+ fileDirectory);
            return (fileDirectoryPath + fileName);
        }

        public GenerationData LoadInputXMLFile(string fileName)
        {
            GenerationData generationData = new GenerationData();
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(GenerationData));

            var inputFilePath = RetrieveFilePath(inputDataFilePath,fileName);

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

        public void WriteOutputToFile(GenerationOutput generationOutputData, string outFileName, string inputFileName)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(GenerationOutput));

            var generatedOutputFileName = retrieveOutputFileName(outFileName);

            var outputFilePath = RetrieveFilePath(outputDataFilePath, generatedOutputFileName);
            var inputFilePath = RetrieveFilePath(inputDataFilePath, inputFileName);
            var archiveFilePath = RetrieveFilePath(archiveDataFilePath, inputFileName);
            try
            {
                TextWriter writer = new StreamWriter(outputFilePath);
                xmlSerializer.Serialize(writer, generationOutputData);
                writer.Close();

                File.Copy(inputFilePath, archiveFilePath);
                File.Delete(inputFilePath);

            } catch (Exception ex)
            {
                Console.WriteLine("Error outputting to file", ex.Message);
            }
        }

        private string retrieveOutputFileName(string filename)
        {
            return filename + "-Result.xml";
        }
    }
}
