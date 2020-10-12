using System;
using Castle.Core.Internal;

namespace Main.Core.InGameEditor.GetEnvironmentFilenameServices
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
            if (environmentName.IsNullOrEmpty())
            {
                throw new ArgumentException($"{nameof(environmentName)} is null or empty");
            }
            
            return environmentName + fileType.Name + AssetFileExtension;
        }
    }
}