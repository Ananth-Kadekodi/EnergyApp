﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnergyApp.EnergyService
{
    public class EnergyAppService 
    {
        public void RunApplication()
        {
            FileManager fileManager = new FileManager();
            ReportProcessorService reportProcessorService = new ReportProcessorService();

            var referenceData = fileManager.LoadReferenceDataFile();
            var generationReportData = fileManager.LoadInputXMLFile();
            var generationOutputData = reportProcessorService.ProcessInputReport(generationReportData, referenceData);
            fileManager.WriteOutputToFile(generationOutputData);
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

