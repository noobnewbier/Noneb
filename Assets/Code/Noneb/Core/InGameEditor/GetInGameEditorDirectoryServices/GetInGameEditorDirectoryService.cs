using System;
using System.IO;
using Castle.Core.Internal;

namespace Noneb.Core.InGameEditor.GetInGameEditorDirectoryServices
{
    public interface IGetInGameEditorDirectoryService
    {
        string GetRelativeDirectoryToSpecificEnvironment(string environmentName);
    }

    public class GetInGameEditorDirectoryService : IGetInGameEditorDirectoryService
    {
        private static string EnvironmentsDirectory => "Data/Environments/";

        public string GetRelativeDirectoryToSpecificEnvironment(string environmentName)
        {
            if (environmentName.IsNullOrEmpty())
            {
                throw new ArgumentException($"{nameof(environmentName)} is null or empty");
            }
            
            return Path.Combine("Assets/", EnvironmentsDirectory, environmentName);
        }
    }
}