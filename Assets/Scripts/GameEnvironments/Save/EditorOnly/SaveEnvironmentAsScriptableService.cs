using System;
using System.IO;
using Common.Constants;
using GameEnvironments.Common;
using GameEnvironments.Common.Data.GameEnvironments;
using GameEnvironments.Common.Data.LevelDatas;
using Maps;
using UnityEditor;
using UnityEngine;
using WorldConfigurations;

namespace GameEnvironments.Save.EditorOnly
{
    public class SaveEnvironmentAsScriptableService : ISaveEnvironmentService
    {
        private const string AssetFileExtension = ".asset";

        public SavingResult TrySaveEnvironment(GameEnvironment gameEnvironment, string environmentName)
        {
            var parentFolder = Path.Combine("Assets/", DirectoryNames.Environments);
            var childFolder = environmentName;
            var path = Path.Combine(parentFolder, childFolder);

            var levelDataFilename = environmentName + nameof(LevelData) + AssetFileExtension;
            var gameEnvironmentFilename = environmentName + nameof(GameEnvironment) + AssetFileExtension;
            var mapConfigFilename = environmentName + nameof(MapConfiguration) + AssetFileExtension;
            var worldConfigFilename = environmentName + nameof(WorldConfiguration) + AssetFileExtension;

            if (!File.Exists(Path.Combine(path, gameEnvironmentFilename)))
            {
                try
                {
                    if (!AssetDatabase.IsValidFolder(path))
                    {
                        AssetDatabase.CreateFolder(parentFolder, childFolder);
                    }

                    CreateAndSaveAsset(
                        gameEnvironment,
                        path,
                        levelDataFilename,
                        gameEnvironmentFilename,
                        mapConfigFilename,
                        worldConfigFilename
                    );

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

            Debug.Log("File already exist");
            return SavingResult.FileExist;
        }

        private static void CreateAndSaveAsset(GameEnvironment gameEnvironment,
                                               string fullPath,
                                               string levelDataFilename,
                                               string gameEnvironmentFilename,
                                               string mapConfigFilename,
                                               string worldConfigFilename)
        {
            //create new instances as they cannot be referencing the old configs
            var levelDataScriptable = LevelDataScriptable.CreateScriptableFromLevelData(
                gameEnvironment.LevelData
            );
            var mapConfiguration = MapConfiguration.Create(
                gameEnvironment.MapConfiguration.XSize,
                gameEnvironment.MapConfiguration.ZSize
            );
            var worldConfiguration = WorldConfiguration.Create(
                gameEnvironment.WorldConfiguration.UpAxis,
                gameEnvironment.WorldConfiguration.InnerRadius
            );

            var gameEnvironmentScriptable = GameEnvironmentScriptable.Create(
                mapConfiguration,
                worldConfiguration,
                levelDataScriptable
            );

            AssetDatabase.CreateAsset(levelDataScriptable, $"{fullPath}/{levelDataFilename}");
            AssetDatabase.CreateAsset(mapConfiguration, $"{fullPath}/{mapConfigFilename}");
            AssetDatabase.CreateAsset(worldConfiguration, $"{fullPath}/{worldConfigFilename}");
            AssetDatabase.CreateAsset(
                gameEnvironmentScriptable,
                $"{fullPath}/{gameEnvironmentFilename}"
            );
        }
    }
}