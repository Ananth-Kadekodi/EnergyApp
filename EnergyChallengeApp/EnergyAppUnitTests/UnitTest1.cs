using EnergyApp.EnergyService;

namespace EnergyAppUnitTests
{
    public class UnitTest1
    {
        private static string referenceDataFileName = "ReferenceData.xml";
        private static string inputDataTestFilePath = "INPUT_TEST_FILE_PATH";
        private static string outputDataTestFilePath = "OUTPUT_TEST_FILE_PATH";
        private static string archiveDataTestFilePath = "ARCHIVE_TEST_FILE_PATH";
        private static string referenceTestFilePath = "REFERENCE_TEST_FILE_PATH";
        private static string inputFileName = "Basic.xml";

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
    }
}