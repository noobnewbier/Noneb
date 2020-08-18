using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using GameEnvironments.Common;
using GameEnvironments.Common.Data;
using GameEnvironments.Common.Data.LevelDatas;
using GameEnvironments.Common.Services.GetEnvironmentFilenameServices;
using GameEnvironments.Common.Services.GetInGameEditorDirectoryService;
using GameEnvironments.Save;
using InGameEditor.Data;
using InGameEditor.Data.Availables;
using InGameEditor.Repositories.SelectedEditorPalettes;
using Maps;
using UnityEngine;
using WorldConfigurations;

namespace ObsoleteJsonRelated
{
    /// <summary>
    /// Try to save the environment as json, return value indicates if it is successful
    /// Prime reason that saving is not successful is that there already exist a file with the same name
    /// </summary>
    /// <returns>Enum indicates whether saving is successful</returns>
    public class SaveEnvironmentAsJsonService : ISaveEnvironmentService
    {
        private readonly ISelectedEditorPaletteRepository _editorPaletteRepository;
        private readonly IGetEnvironmentFilenameService _getEnvironmentFilenameService;
        private readonly IGetInGameEditorDirectoryService _getInGameEditorDirectoryService;

        public SaveEnvironmentAsJsonService(ISelectedEditorPaletteRepository editorPaletteRepository,
                                            IGetEnvironmentFilenameService getEnvironmentFilenameService,
                                            IGetInGameEditorDirectoryService getInGameEditorDirectoryService)
        {
            _editorPaletteRepository = editorPaletteRepository;
            _getEnvironmentFilenameService = getEnvironmentFilenameService;
            _getInGameEditorDirectoryService = getInGameEditorDirectoryService;
        }

        public SavingResult TrySaveEnvironment(GameEnvironment gameEnvironment, string environmentName)
        {
            var parentFolder = _getInGameEditorDirectoryService.GetFullDirectoryToSpecificEnvironment(environmentName);
            var fullPath = Path.Combine(parentFolder, _getEnvironmentFilenameService.GetEnvironmentAsJsonFilename(environmentName));

            if (!File.Exists(fullPath))
            {
                var environmentJson = CreateEnvironmentJson(gameEnvironment, _editorPaletteRepository.Palette);
                var environmentJsonString = JsonUtility.ToJson(environmentJson);

                try
                {
                    Directory.CreateDirectory(parentFolder);
                    File.WriteAllText(fullPath, environmentJsonString);
                    return SavingResult.Success;
                }
                catch (ArgumentException)
                {
                    Debug.Log($"path: {fullPath} is invalidly formed");
                    return SavingResult.Error;
                }
                catch (PathTooLongException)
                {
                    Debug.Log($"path: {fullPath} is too long");
                    return SavingResult.Error;
                }
                catch (DirectoryNotFoundException)
                {
                    Debug.Log($"path: {fullPath} is invalid, is it on an unmapped drive?(FWIW I don't even know what is an unmapped drive");
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

        private static GameEnvironmentJson CreateEnvironmentJson(GameEnvironment gameEnvironment, EditorPalette editorPalette)
        {
            var mapConfiguration = gameEnvironment.MapConfiguration;
            var datasLength = mapConfiguration.GetTotalMapSize();

            var levelDataJson = CreateLevelDataJson(gameEnvironment.LevelData, datasLength, editorPalette);
            var mapConfigJson = CreateMapConfigurationJson(gameEnvironment.MapConfiguration);
            var worldConfigJson = CreateWorldConfigurationJson(gameEnvironment.WorldConfiguration);

            return new GameEnvironmentJson(
                mapConfigJson,
                worldConfigJson,
                levelDataJson,
                gameEnvironment.EnvironmentName
            );
        }

        private static WorldConfigurationJson CreateWorldConfigurationJson(WorldConfig worldConfiguration)
        {
            return new WorldConfigurationJson(worldConfiguration.InnerRadius, worldConfiguration.UpAxis);
        }

        private static MapConfigurationJson CreateMapConfigurationJson(MapConfig mapConfiguration)
        {
            return new MapConfigurationJson(mapConfiguration.XSize, mapConfiguration.ZSize);
        }

        private static LevelDataJson CreateLevelDataJson(LevelData levelData, int datasLength, EditorPalette editorPalette)
        {
            var tileDataAsInts = new int[datasLength];
            var tileGameObjectAsInts = new int[datasLength];
            var constructDataAsInts = new int[datasLength];
            var constructGameObjectAsInts = new int[datasLength];
            var unitDataAsInts = new int[datasLength];
            var unitGameObjectAsInts = new int[datasLength];
            var strongholdUnitDataAsInts = new int[datasLength];
            var strongholdUnitGameObjectProvidersAsInts = new int[datasLength];
            var strongholdConstructDataAsInts = new int[datasLength];
            var strongholdConstructGameObjectProvidersAsInts = new int[datasLength];

            FillArrayWithMatchingIndex(editorPalette.AvailableTileDatas, levelData.TileDatas, tileDataAsInts);
            FillArrayWithMatchingIndex(
                editorPalette.AvailableTileGameObjectProviders,
                levelData.TileGameObjectProviders,
                tileGameObjectAsInts
            );

            FillArrayWithMatchingIndex(editorPalette.AvailableConstructDatas, levelData.ConstructDatas, constructDataAsInts);
            FillArrayWithMatchingIndex(
                editorPalette.AvailableConstructGameObjectProviders,
                levelData.ConstructGameObjectProviders,
                constructGameObjectAsInts
            );

            FillArrayWithMatchingIndex(editorPalette.AvailableUnitDatas, levelData.UnitDatas, unitDataAsInts);
            FillArrayWithMatchingIndex(
                editorPalette.AvailableUnitGameObjectProviders,
                levelData.UnitGameObjectProviders,
                unitGameObjectAsInts
            );

            FillArrayWithMatchingIndex(
                editorPalette.AvailableConstructDatas,
                levelData.StrongholdDatas.Select(d => d?.ConstructData).ToList(),
                strongholdConstructDataAsInts
            );
            FillArrayWithMatchingIndex(
                editorPalette.AvailableConstructGameObjectProviders,
                levelData.StrongholdConstructGameObjectProviders,
                strongholdConstructGameObjectProvidersAsInts
            );

            FillArrayWithMatchingIndex(
                editorPalette.AvailableUnitDatas,
                levelData.StrongholdDatas.Select(d => d?.UnitData).ToList(),
                strongholdUnitDataAsInts
            );
            FillArrayWithMatchingIndex(
                editorPalette.AvailableUnitGameObjectProviders,
                levelData.StrongholdUnitGameObjectProviders,
                strongholdUnitGameObjectProvidersAsInts
            );


            return new LevelDataJson(
                tileDataAsInts,
                tileGameObjectAsInts,
                constructDataAsInts,
                constructGameObjectAsInts,
                unitDataAsInts,
                unitGameObjectAsInts,
                strongholdUnitDataAsInts,
                strongholdUnitGameObjectProvidersAsInts,
                strongholdConstructDataAsInts,
                strongholdConstructGameObjectProvidersAsInts
            );
        }

        private static void FillArrayWithMatchingIndex<T>(AvailableSet<T> availableSet, IReadOnlyList<T> datas, IList<int> arrayToFill)
        {
            var availableSetData = availableSet.Set;
            for (var i = 0; i < datas.Count; i++)
            {
                var index = Array.IndexOf(availableSetData, datas[i]);

                if (index == -1 && datas[i] != null) //if there is a data, but could not be found in palette
                {
                    throw new InvalidDataException($"In {typeof(T).Name}'s data, entry {i} cannot be seen in the editor palette");
                }

                arrayToFill[i] = index;
            }
        }
    }
}