using System;
using System.IO;
using Noneb.Core.Game.GameEnvironments.Data;
using Noneb.Core.Game.GameEnvironments.Data.LevelDatas;
using Noneb.Core.Game.GameEnvironments.Save;
using Noneb.Core.Game.Maps;
using Noneb.Core.Game.WorldConfigurations;
using Noneb.Core.InGameEditor.GetEnvironmentFilename;
using Noneb.Core.InGameEditor.GetInGameEditorDirectory;
using UnityEditor;
using UnityEngine;

namespace Noneb.Core.InGameEditor.Save.EditorOnly
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
            var path = _getInGameEditorDirectoryService.GetRelativeDirectoryToSpecificEnvironment(environmentName);

            var levelDataFilename = _environmentFilenameService.GetEnvironmentAsScriptableFilename(environmentName, typeof(LevelData));
            var gameEnvironmentFilename = _environmentFilenameService.GetEnvironmentAsScriptableFilename(environmentName, typeof(GameEnvironment));
            var mapConfigFilename = _environmentFilenameService.GetEnvironmentAsScriptableFilename(environmentName, typeof(MapConfig));
            var worldConfigFilename = _environmentFilenameService.GetEnvironmentAsScriptableFilename(environmentName, typeof(WorldConfig));

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
            var levelDataScriptable = LevelDataScriptable.Create(
                gameEnvironment.LevelData
            );
            var mapConfiguration = MapConfig.Create(
                gameEnvironment.MapConfiguration.GetMap2DActualWidth(),
                gameEnvironment.MapConfiguration.GetMap2DActualHeight()
            );
            var worldConfiguration = WorldConfig.Create(
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