namespace EnergyApp.EnergyService
{
    public class EnergyAppService : IEnergyAppService
    {
        private static string inputDataFileDirectory = "INPUT_FILE_DIRECTORY";
        private static string referenceDataFileName = "ReferenceData.xml";
        private static string referenceDataFilePath = "REFERENCE_FILE_PATH";
        private static string inputDataFilePath = "INPUT_FILE_PATH";
        private static string outputDataFilePath = "OUTPUT_FILE_PATH";
        private static string archiveDataFilePath = "ARCHIVE_FILE_PATH";

        public void RunApplication()
        {
            FileManager fileManager = new FileManager();
            ReportProcessorService reportProcessorService = new ReportProcessorService();

            var inputFileDirectory = fileManager.RetrieveFilePath(inputDataFileDirectory, "");

            string[] inputFiles = Directory.GetFiles(inputFileDirectory);

            foreach (string inputFile in inputFiles)
            {
                var inputFileName = Path.GetFileName(inputFile);
                var inputFileNameWithoutExt = Path.GetFileNameWithoutExtension(inputFile);

                var referenceData = fileManager.LoadReferenceDataFile(referenceDataFileName, referenceDataFilePath);
                var generationReportData = fileManager.LoadInputXMLFile(inputFileName, inputDataFilePath);
                var generationOutputData = reportProcessorService.ProcessInputReport(generationReportData, referenceData);
                fileManager.WriteOutputToFile(generationOutputData, inputFileNameWithoutExt, inputFileName, outputDataFilePath, inputDataFilePath, archiveDataFilePath);
            }
        }

        public void MonitorDirectory(string path)
        {
            Console.WriteLine("Monitoring the input folder");

            var watcher = new FileSystemWatcher();
            watcher.Path = path;
            watcher.NotifyFilter = NotifyFilters.LastWrite;
            watcher.Changed += FileCreatedInMonitoredDirectory;
            watcher.EnableRaisingEvents = true;
        }

        private void FileCreatedInMonitoredDirectory(object sender, FileSystemEventArgs e)
        {
            var fileName = e.FullPath;

            if (!File.Exists(fileName))
            {
                return;
            }

            Console.WriteLine("New file found - running application");
            RunApplication();
        }
    }
}

