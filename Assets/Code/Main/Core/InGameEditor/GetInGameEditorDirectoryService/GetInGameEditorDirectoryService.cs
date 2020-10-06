using System.IO;
using UnityEngine;

namespace Main.Core.InGameEditor.GetInGameEditorDirectoryService
{
    public interface IGetInGameEditorDirectoryService
    {
        string GetParentDirectoryForAllEnvironmentsInEditor();
        string GetRelativeDirectoryFromParentToSpecificEnvironment(string environmentName);
        string GetFullDirectoryToSpecificEnvironment(string environmentName);
        string GetRelativeDirectoryToSpecificEnvironmentForAssetDatabase(string environmentName);
    }

    public class GetInGameEditorDirectoryService : IGetInGameEditorDirectoryService
    {
        private static string Environments => "Data/Environments";

        public string GetParentDirectoryForAllEnvironmentsInEditor()
        {
            var pathPrefix = Application.isEditor ? Application.dataPath : Application.persistentDataPath;

            return Path.Combine(pathPrefix, Environments);
        }

        public string GetRelativeDirectoryToSpecificEnvironmentForAssetDatabase(string environmentName) =>
            Path.Combine("Assets/", Environments, environmentName);

        public string GetRelativeDirectoryFromParentToSpecificEnvironment(string environmentName) => environmentName;

        public string GetFullDirectoryToSpecificEnvironment(string environmentName) => Path.Combine(
            GetParentDirectoryForAllEnvironmentsInEditor(),
            GetRelativeDirectoryFromParentToSpecificEnvironment(environmentName)
        );
    }
}