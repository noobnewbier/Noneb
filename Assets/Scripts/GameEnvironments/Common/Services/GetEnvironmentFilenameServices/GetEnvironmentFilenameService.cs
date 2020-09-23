using System;

namespace GameEnvironments.Common.Services.GetEnvironmentFilenameServices
{
    public interface IGetEnvironmentFilenameService
    {
        string GetEnvironmentAsScriptableFilename(string environmentName, Type fileType);
    }

    public class GetEnvironmentFilenameService : IGetEnvironmentFilenameService
    {
        private const string AssetFileExtension = ".asset";

        public string GetEnvironmentAsScriptableFilename(string environmentName, Type fileType)
        {
            return environmentName + fileType.Name + AssetFileExtension;
        }
    }
}