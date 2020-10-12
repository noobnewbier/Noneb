using System.IO;

namespace Main.Core.InGameEditor.GetInGameEditorDirectoryServices
{
    public interface IGetInGameEditorDirectoryService
    {
        string GetRelativeDirectoryToSpecificEnvironmentForAssetDatabase(string environmentName);
    }

    public class GetInGameEditorDirectoryService : IGetInGameEditorDirectoryService
    {
        private static string Environments => "Data/Environments";

        public string GetRelativeDirectoryToSpecificEnvironmentForAssetDatabase(string environmentName) =>
            Path.Combine("Assets/", Environments, environmentName);
    }
}