using System;

namespace Main.Core.InGameEditor.GetEnvironmentFilenameServices
{
    public interface IGetEnvironmentFilenameService
    {
        string GetEnvironmentAsScriptableFilename(string environmentName, Type fileType);
    }

    public class GetEnvironmentFilenameService : IGetEnvironmentFilenameService
    {
        private const string AssetFileExtension = ".asset";

        public string GetEnvironmentAsScriptableFilename(string environmentName, Type fileType) =>
            environmentName + fileType.Name + AssetFileExtension;
    }
}