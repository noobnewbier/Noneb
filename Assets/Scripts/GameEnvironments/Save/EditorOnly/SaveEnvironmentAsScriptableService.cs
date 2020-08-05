using System;
using System.IO;
using Common.Constants;
using GameEnvironments.Common;
using GameEnvironments.Common.Data;
using UnityEditor;
using UnityEngine;

namespace GameEnvironments.Save.EditorOnly
{
    public class SaveEnvironmentAsScriptableService : ISaveEnvironmentService
    {
        private const string AssetFileExtension = ".asset";
        
        public SavingResult TrySaveEnvironment(GameEnvironment gameEnvironment, string filename)
        {
            var path = Path.Combine(DirectoryNames.Environments, filename + AssetFileExtension);
            if (!File.Exists(path))
            {
                try
                {
                    AssetDatabase.CreateAsset(GameEnvironmentScriptable.CreateScriptableFromGameEnvironment(gameEnvironment), path);
                    return SavingResult.Success;
                }
                catch (ArgumentException)
                {
                    Debug.Log($"path: {path} is invalidly formed");
                    return SavingResult.Error;
                }
                catch (PathTooLongException)
                {
                    Debug.Log($"path: {path} is too long");
                    return SavingResult.Error;
                }
                catch (DirectoryNotFoundException)
                {
                    Debug.Log($"path: {path} is invalid, is it on an unmapped drive?(FWIW I don't even know what is an unmapped drive");
                    return SavingResult.Error;
                }
                catch (IOException)
                {
                    Debug.Log("IO exception occurred, please try again");
                    return SavingResult.Error;
                }
                catch (UnauthorizedAccessException)
                {
                    Debug.Log("Do not have the required privilege");
                    return SavingResult.Error;
                }
            }

            return SavingResult.FileExist;
        }
    }
}