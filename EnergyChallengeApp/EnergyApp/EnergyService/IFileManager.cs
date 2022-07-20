using EnergyApp.DataModels;

namespace EnergyApp.EnergyService
{
    public interface IFileManager
    {
        GenerationData LoadInputXMLFile(string fileName);
        ReferenceData LoadReferenceDataFile(string fileName);
        string RetrieveFilePath(string fileDirectory, string fileName);
        void WriteOutputToFile(GenerationOutput generationOutputData, string outFileName, string inputFileName);
    }
}