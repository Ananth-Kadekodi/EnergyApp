using EnergyApp.EnergyService;

namespace EnergyAppUnitTests
{
    public class UnitTest1
    {
        private static string referenceDataFileName = "ReferenceData.xml";
        private static string inputFileName = "Basic.xml";

        [Fact]
        public void AssessXMLFileGenerated()
        {
            /*FileManager fileManager = new FileManager();
            ReportProcessorService reportProcessorService = new ReportProcessorService();

            var referenceData = fileManager.LoadReferenceDataFile(referenceDataFileName);
            var generationReportData = fileManager.LoadInputXMLFile(inputFileName);
            var generationOutputData = reportProcessorService.ProcessInputReport(generationReportData, referenceData);
            */
        }
    }
}