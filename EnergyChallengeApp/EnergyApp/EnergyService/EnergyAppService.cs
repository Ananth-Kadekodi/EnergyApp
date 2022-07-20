namespace EnergyApp.EnergyService
{
    public class EnergyAppService 
    {
        private static string inputDataFileDirectory = "INPUT_FILE_DIRECTORY";
        private static string referenceDataFileName = "ReferenceData.xml";
        public void RunApplication()
        {
            FileManager fileManager = new FileManager();
            ReportProcessorService reportProcessorService = new ReportProcessorService();

            var inputFileDirectory = fileManager.RetrieveFilePath(inputDataFileDirectory, "");

            string[] inputFiles = Directory.GetFiles(inputFileDirectory);
            
            foreach(string inputFile in inputFiles)
            {
                var inputFileName = Path.GetFileName(inputFile);
                var inputFileNameWithoutExt = Path.GetFileNameWithoutExtension(inputFile);

                var referenceData = fileManager.LoadReferenceDataFile(referenceDataFileName);
                var generationReportData = fileManager.LoadInputXMLFile(inputFileName);
                var generationOutputData = reportProcessorService.ProcessInputReport(generationReportData, referenceData);
                fileManager.WriteOutputToFile(generationOutputData, inputFileNameWithoutExt, inputFileName);
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

