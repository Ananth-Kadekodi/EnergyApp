using EnergyApp.DataModels;

namespace EnergyApp.EnergyService
{
    public interface IFileManager
    {
        GenerationData LoadInputXMLFile(string fileName, string inputDataFilePath);
        ReferenceData LoadReferenceDataFile(string fileName, string referenceDataFilePath);
        string RetrieveFilePath(string fileDirectory, string fileName);
        void WriteOutputToFile(GenerationOutput generationOutputData, string outFileName, string inputFileName, string outputDataFilePath, string inputDataFilePath, string archiveDataFilePath);
    }
}