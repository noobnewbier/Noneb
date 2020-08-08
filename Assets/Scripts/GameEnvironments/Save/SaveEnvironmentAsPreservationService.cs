using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Common.Constants;
using GameEnvironments.Common;
using GameEnvironments.Common.Data;
using InGameEditor.Data;
using InGameEditor.Data.Availables;
using InGameEditor.Repositories.SelectedEditorPalettes;
using UnityEngine;

namespace GameEnvironments.Save
{
    /// <summary>
    /// Try to save the environment as json, return value indicates if it is successful
    /// Prime reason that saving is not successful is that there already exist a file with the same name
    /// </summary>
    /// <returns>Enum indicates whether saving is successful</returns>
    public class SaveEnvironmentAsPreservationService : ISaveEnvironmentService
    {
        private const string JsonFileExtension = ".json";

        private readonly ISelectedEditorPaletteRepository _editorPaletteRepository;

        public SaveEnvironmentAsPreservationService(ISelectedEditorPaletteRepository editorPaletteRepository)
        {
            _editorPaletteRepository = editorPaletteRepository;
        }

        public SavingResult TrySaveEnvironment(GameEnvironment gameEnvironment, string environmentName)
        {
            var pathPrefix = Application.isEditor ? Application.dataPath : Application.persistentDataPath;
            var parentFolder = Path.Combine(pathPrefix, DirectoryNames.Environments, $"{environmentName}/");
            var fullPath = Path.Combine(parentFolder, $"{environmentName}{JsonFileExtension}");

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
                catch (DirectoryNotFoundException e)
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
            var arrayLength = mapConfiguration.GetTotalMapSize();
            var levelData = gameEnvironment.LevelData;

            var tileDataAsInts = new int[arrayLength];
            var tileGameObjectAsInts = new int[arrayLength];
            var constructDataAsInts = new int[arrayLength];
            var constructGameObjectAsInts = new int[arrayLength];
            var unitDataAsInts = new int[arrayLength];
            var unitGameObjectAsInts = new int[arrayLength];

            FillArrayWithMatchingIndex(editorPalette.AvailableTileData, levelData.TileDatas, tileDataAsInts);
            FillArrayWithMatchingIndex(
                editorPalette.AvailableTileGameObjectProviders,
                levelData.TileGameObjectProviders,
                tileGameObjectAsInts
            );
            FillArrayWithMatchingIndex(editorPalette.AvailableConstructData, levelData.ConstructDatas, constructDataAsInts);
            FillArrayWithMatchingIndex(
                editorPalette.AvailableConstructGameObjectProviders,
                levelData.ConstructGameObjectProviders,
                constructGameObjectAsInts
            );
            FillArrayWithMatchingIndex(editorPalette.AvailableUnitData, levelData.UnitDatas, unitDataAsInts);
            FillArrayWithMatchingIndex(
                editorPalette.AvailableUnitGameObjectProviders,
                levelData.UnitGameObjectProviders,
                unitGameObjectAsInts
            );

            return new GameEnvironmentJson(
                tileDataAsInts,
                tileGameObjectAsInts,
                constructDataAsInts,
                constructGameObjectAsInts,
                unitDataAsInts,
                unitGameObjectAsInts,
                gameEnvironment.WorldConfiguration.InnerRadius,
                mapConfiguration.GetMap2DArrayWidth(),
                mapConfiguration.GetMap2DArrayHeight(),
                gameEnvironment.WorldConfiguration.UpAxis
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