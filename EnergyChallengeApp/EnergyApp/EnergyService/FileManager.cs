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
            //XmlRootAttribute xRoot = new XmlRootAttribute();
            //xRoot.ElementName = "GenerationReport";
            // xRoot.Namespace = "http://www.cpandl.com";
            //xRoot.IsNullable = true;
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(GenerationData));
            

            var referenceDataConfig = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            var referenceFilePath = referenceDataConfig.GetValue<string>("AppSettings:INPUT_FILE_PATH");
            var referenceFileName = referenceDataConfig.GetValue<string>("AppSettings:INPUT_FILE_NAME");

            //var abc = ReadFileAsString(referenceFilePath, referenceFileName);
            //var hello = DeserializeGenerationReportData(abc);
            using (Stream reader = new FileStream(referenceFilePath, FileMode.Open))
            {
                generationData = (GenerationData)xmlSerializer.Deserialize(reader);
            }

            return generationData;
        }


        public string ReadFileAsString(string folderPath, string filename)
        {

            var fileToLoad = GetFileInfo(folderPath, filename);
            var result = File.ReadAllText(fileToLoad.FullName);
            return result;
        }

        private FileInfo GetFileInfo(string folderPath, string filename)
        {


            var path = Path.Join(folderPath, filename);
            var result = new FileInfo(path);
            return result;
        }

        public GenerationData DeserializeGenerationReportData(string generationReportDataXml)
        {
            var result = DeserializeData<GenerationData>(generationReportDataXml);
            return result;
        }

        private T DeserializeData<T>(string xmlData)
        {
            var xmlSerializer = new XmlSerializer(typeof(T));
            using var stringReader = new StringReader(xmlData);
            var result = (T)xmlSerializer.Deserialize(stringReader);
            return result;
        }
    }
}
