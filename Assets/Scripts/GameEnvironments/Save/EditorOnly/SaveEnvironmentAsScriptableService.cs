using System;
using System.IO;
using GameEnvironments.Common;
using GameEnvironments.Common.Data;
using GameEnvironments.Common.Data.LevelDatas;
using GameEnvironments.Common.Services.GetEnvironmentFilenameServices;
using GameEnvironments.Common.Services.GetInGameEditorDirectoryService;
using Maps;
using UnityEditor;
using UnityEngine;
using WorldConfigurations;

namespace GameEnvironments.Save.EditorOnly
{
    public class SaveEnvironmentAsScriptableService : ISaveEnvironmentService
    {
        private readonly IGetEnvironmentFilenameService _environmentFilenameService;
        private readonly IGetInGameEditorDirectoryService _getInGameEditorDirectoryService;

        public SaveEnvironmentAsScriptableService(IGetEnvironmentFilenameService environmentFilenameService,
                                                  IGetInGameEditorDirectoryService getInGameEditorDirectoryService)
        {
            _environmentFilenameService = environmentFilenameService;
            _getInGameEditorDirectoryService = getInGameEditorDirectoryService;
        }

        public SavingResult TrySaveEnvironment(GameEnvironment gameEnvironment, string environmentName)
        {
            var path = _getInGameEditorDirectoryService.GetRelativeDirectoryToSpecificEnvironmentForAssetDatabase(environmentName);

            var levelDataFilename = _environmentFilenameService.GetEnvironmentAsScriptableFilename(environmentName, typeof(LevelData));
            var gameEnvironmentFilename = _environmentFilenameService.GetEnvironmentAsScriptableFilename(environmentName, typeof(GameEnvironment));
            var mapConfigFilename = _environmentFilenameService.GetEnvironmentAsScriptableFilename(environmentName, typeof(MapConfiguration));
            var worldConfigFilename = _environmentFilenameService.GetEnvironmentAsScriptableFilename(environmentName, typeof(WorldConfiguration));

            if (!File.Exists(Path.Combine(path, gameEnvironmentFilename)))
            {
                try
                {
                    if (!AssetDatabase.IsValidFolder(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    CreateAndSaveAsset(
                        gameEnvironment,
                        path,
                        levelDataFilename,
                        gameEnvironmentFilename,
                        mapConfigFilename,
                        worldConfigFilename,
                        environmentName
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
                                               string worldConfigFilename,
                                               string environmentName)
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
                environmentName,
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