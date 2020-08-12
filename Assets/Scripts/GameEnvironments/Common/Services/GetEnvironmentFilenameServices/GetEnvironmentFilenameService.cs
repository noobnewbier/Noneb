using System;

namespace GameEnvironments.Common.Services.GetEnvironmentFilenameServices
{
    public interface IGetEnvironmentFilenameService
    {
        string GetEnvironmentAsJsonFilename(string environmentName);
        string GetEnvironmentAsScriptableFilename(string environmentName, Type fileType);
    }

    public class GetEnvironmentFilenameService : IGetEnvironmentFilenameService
    {
        private const string AssetFileExtension = ".asset";
        private const string JsonFileExtension = ".json";

        public string GetEnvironmentAsJsonFilename(string environmentName)
        {
            return environmentName + JsonFileExtension;
        }

        public string GetEnvironmentAsScriptableFilename(string environmentName, Type fileType)
        {
            return environmentName + fileType.Name + AssetFileExtension;
        }
    }
}