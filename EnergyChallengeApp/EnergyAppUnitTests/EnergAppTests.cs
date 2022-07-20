using EnergyApp.DataModels;
using EnergyApp.EnergyService;
using System.Xml.Serialization;

namespace EnergyAppUnitTests
{
    public class EnergAppTests
    {
        private static string referenceDataFileName = "ReferenceData.xml";
        private static string inputDataTestFilePath = "INPUT_TEST_FILE_PATH";
        private static string outputDataTestFilePath = "OUTPUT_TEST_FILE_PATH";
        private static string archiveDataTestFilePath = "ARCHIVE_TEST_FILE_PATH";
        private static string referenceTestFilePath = "REFERENCE_TEST_FILE_PATH";
        private static string inputFileName = "Basic.xml";
        private static string outputFileName = "Basic-Result.xml";
        private static string inputFileNameWithoutExt = "Basic";

        [Fact]
        public void AssessXMLReferenceFileExtraction()
        {
            FileManager fileManager = new FileManager();
            ReportProcessorService reportProcessorService = new ReportProcessorService();

            var referenceData = fileManager.LoadReferenceDataFile(referenceDataFileName,referenceTestFilePath );
            var generationReportData = fileManager.LoadInputXMLFile(inputFileName, inputDataTestFilePath);
            var generationOutputData = reportProcessorService.ProcessInputReport(generationReportData, referenceData);

            Assert.Equal(0.812, referenceData.Factors.EmissionsFactor.High);
            Assert.Equal(0.312, referenceData.Factors.EmissionsFactor.Low);
            Assert.Equal(0.562, referenceData.Factors.EmissionsFactor.Medium);
            Assert.Equal(0.946, referenceData.Factors.ValueFactor.High);
            Assert.Equal(0.265, referenceData.Factors.ValueFactor.Low);
            Assert.Equal(0.696, referenceData.Factors.ValueFactor.Medium);
        }

        [Fact]
        public void AssessXMLInputFileExtraction()
        {
            FileManager fileManager = new FileManager();
            ReportProcessorService reportProcessorService = new ReportProcessorService();

            var referenceData = fileManager.LoadReferenceDataFile(referenceDataFileName, referenceTestFilePath);
            var generationReportData = fileManager.LoadInputXMLFile(inputFileName, inputDataTestFilePath);
            var generationOutputData = reportProcessorService.ProcessInputReport(generationReportData, referenceData);

            Assert.Equal(11.815, generationReportData.Coal.CoalGeneratorData[0].ActualNetGeneration);
            Assert.Equal(0.482, generationReportData.Coal.CoalGeneratorData[0].EmissionsRating);
            Assert.Equal(3, generationReportData.Coal.CoalGeneratorData[0].Generation.DayGenerations.Count);
            Assert.Equal(11.815, generationReportData.Coal.CoalGeneratorData[0].TotalHeatInput);

            Assert.Equal(0.038, generationReportData.Gas.GasGeneratorData[0].EmissionsRating);
            Assert.Equal(3, generationReportData.Gas.GasGeneratorData[0].Generation.DayGenerations.Count);

            Assert.Equal(2, generationReportData.Wind.WindGenerator.Count);
            Assert.Equal(3, generationReportData.Wind.WindGenerator[0].Generation.DayGenerations.Count);
            Assert.Equal("Offshore", generationReportData.Wind.WindGenerator[0].Location);

            Assert.Equal(3, generationReportData.Wind.WindGenerator[1].Generation.DayGenerations.Count);
            Assert.Equal("Onshore", generationReportData.Wind.WindGenerator[1].Location);
        }

        [Fact]
        public void AssessXMLOutputFile()
        {
            FileManager fileManager = new FileManager();
            ReportProcessorService reportProcessorService = new ReportProcessorService();

            var referenceData = fileManager.LoadReferenceDataFile(referenceDataFileName, referenceTestFilePath);
            var generationReportData = fileManager.LoadInputXMLFile(inputFileName, inputDataTestFilePath);
            var generationOutputData = reportProcessorService.ProcessInputReport(generationReportData, referenceData);
            fileManager.WriteOutputToFile(generationOutputData, inputFileNameWithoutExt, inputFileName, outputDataTestFilePath, inputDataTestFilePath, archiveDataTestFilePath);

            GenerationOutput generationData = new GenerationOutput();
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(GenerationOutput));

            var inputFilePath = fileManager.RetrieveFilePath(outputDataTestFilePath, outputFileName);

            using (Stream reader = new FileStream(inputFilePath, FileMode.Open))
            {
                generationData = (GenerationOutput)xmlSerializer.Deserialize(reader);
            }

            Assert.Equal(1, generationData.ActualHeatRatesData.GeneratorActualHeatRates[0].HeatRate);
            Assert.Equal(1, generationData.ActualHeatRatesData.GeneratorActualHeatRates.Count);
            
            Assert.Equal(3, generationData.MaxEmissionGeneratorsData.GeneratorDayEmissions.Count);
            Assert.Equal(5.1323807, generationData.MaxEmissionGeneratorsData.GeneratorDayEmissions[0].Emission);
            Assert.Equal(137.175004008, generationData.MaxEmissionGeneratorsData.GeneratorDayEmissions[1].Emission);
            Assert.Equal(136.440767624, generationData.MaxEmissionGeneratorsData.GeneratorDayEmissions[2].Emission);

            Assert.Equal(4, generationData.Totals.GeneratorTotalValues.Count);
            Assert.Equal(1662.6174457050001, generationData.Totals.GeneratorTotalValues[0].Total);
            Assert.Equal("Wind[Offshore]", generationData.Totals.GeneratorTotalValues[0].Name);

            Assert.Equal(4869.4539173939993, generationData.Totals.GeneratorTotalValues[1].Total);
            Assert.Equal("Wind[Onshore]", generationData.Totals.GeneratorTotalValues[1].Name);

            Assert.Equal(8512.25460552, generationData.Totals.GeneratorTotalValues[2].Total);
            Assert.Equal("Gas[1]", generationData.Totals.GeneratorTotalValues[2].Name);

            Assert.Equal(5341.7165266320008, generationData.Totals.GeneratorTotalValues[3].Total);
            Assert.Equal("Coal[1]", generationData.Totals.GeneratorTotalValues[3].Name);
        }
    }
}