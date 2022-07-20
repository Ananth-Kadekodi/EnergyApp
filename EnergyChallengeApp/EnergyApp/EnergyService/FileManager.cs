using EnergyApp.DataModels;
using Microsoft.Extensions.Configuration;
using System.Xml.Serialization;

namespace EnergyApp.EnergyService
{
    public class FileManager 
    {
        public ReferenceData LoadReferenceDataFile(string fileName, string referenceDataFilePath)
        {
            ReferenceData referenceData = new ReferenceData();
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ReferenceData));

            var referenceFilePath = RetrieveFilePath(referenceDataFilePath, fileName);

            try
            {
                using (Stream reader = new FileStream(referenceFilePath, FileMode.Open))
                {
                    referenceData = (ReferenceData)xmlSerializer.Deserialize(reader);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception retrieving reference file data", ex.Message);
            }

            return referenceData;
        }

        public string RetrieveFilePath(string fileDirectory, string fileName)
        {
            var referenceDataConfig = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            var fileDirectoryPath = referenceDataConfig.GetValue<string>("AppSettings:" + fileDirectory);
            return (fileDirectoryPath + fileName);
        }

        public GenerationData LoadInputXMLFile(string fileName, string inputDataFilePath)
        {
            GenerationData generationData = new GenerationData();
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(GenerationData));

            var inputFilePath = RetrieveFilePath(inputDataFilePath, fileName);

            try
            {
                using (Stream reader = new FileStream(inputFilePath, FileMode.Open))
                {
                    generationData = (GenerationData)xmlSerializer.Deserialize(reader);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception retrieving input data file", ex.Message);
            }

            return generationData;
        }

        public void WriteOutputToFile(GenerationOutput generationOutputData, string outFileName, string inputFileName, string outputDataFilePath, string inputDataFilePath, string archiveDataFilePath)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(GenerationOutput));

            var generatedOutputFileName = retrieveOutputFileName(outFileName);
            var generatedArchiveFileName = retrieveArchiveFileName(outFileName);

            var outputFilePath = RetrieveFilePath(outputDataFilePath, generatedOutputFileName);
            var inputFilePath = RetrieveFilePath(inputDataFilePath, inputFileName);
            var archiveFilePath = RetrieveFilePath(archiveDataFilePath, generatedArchiveFileName);
            try
            {
                TextWriter writer = new StreamWriter(outputFilePath);
                xmlSerializer.Serialize(writer, generationOutputData);
                writer.Close();

                File.Copy(inputFilePath, archiveFilePath);
                File.Delete(inputFilePath);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error outputting to file", ex.Message);
            }
        }

        private string retrieveOutputFileName(string fileName)
        {
            return filename + "-Result.xml";
        }

        private string retrieveArchiveFileName(string fileName)
        {
            return filename + DateTime.Now.ToFileTime() + ".xml"; 
        }
    }
}
