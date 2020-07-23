using System;
using System.Collections.Generic;
using System.IO;
using Common.Constants;
using InGameEditor.Data;
using InGameEditor.Data.Availables;
using Maps;
using Maps.Data;
using Maps.Repositories;
using UnityEngine;

namespace InGameEditor.Services.SaveEnvironment
{
    public interface ISaveEnvironmentAsPreservationService
    {
        /// <summary>
        /// Try to save the environment as json, return value indicates if it is successful
        /// Prime reason that saving is not successful is that there already exist a file with the same name
        /// </summary>
        /// <returns>Enum indicates whether saving is successful</returns>
        SavingResult TrySaveEnvironment(GameEnvironment gameEnvironment, EditorPalette editorPalette, string filename);
    }

    public enum SavingResult
    {
        Success,
        FileExist,
        Error
    }

    public class SaveEnvironmentAsPreservationService : ISaveEnvironmentAsPreservationService
    {
        private const string JsonFileExtension = ".json";

        private readonly IMapCharacteristicRepository _mapCharacteristicRepository;

        public SaveEnvironmentAsPreservationService(IMapCharacteristicRepository mapCharacteristicRepository)
        {
            _mapCharacteristicRepository = mapCharacteristicRepository;
        }

        public SavingResult TrySaveEnvironment(GameEnvironment gameEnvironment, EditorPalette editorPalette, string filename)
        {
            var path = Path.Combine(DirectoryNames.Environments, filename + JsonFileExtension);
            if (!File.Exists(path))
            {
                var environmentJson = CreateEnvironmentJson(gameEnvironment, editorPalette);
                var environmentJsonString = JsonUtility.ToJson(environmentJson);

                try
                {
                    File.WriteAllText(path, environmentJsonString);
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

        private GameEnvironmentJson CreateEnvironmentJson(GameEnvironment gameEnvironment, EditorPalette editorPalette)
        {
            var arrayLength = _mapCharacteristicRepository.GetMap2DArrayWidth();
            var upAxis = _mapCharacteristicRepository.GetUpAxis();
            
            var tileDataAsInts = new int[arrayLength];
            var tileRepresentationAsInts = new int[arrayLength];
            var constructDataAsInts = new int[arrayLength];
            var constructRepresentationAsInts = new int[arrayLength];
            var unitDataAsInts = new int[arrayLength];
            var unitRepresentationAsInts = new int[arrayLength];

            FillArrayWithMatchingIndex(editorPalette.AvailableTileData, gameEnvironment.TileDatas, tileDataAsInts);
            FillArrayWithMatchingIndex(
                editorPalette.AvailableTileRepresentationProviders,
                gameEnvironment.TileRepresentationProviders,
                tileRepresentationAsInts
            );
            FillArrayWithMatchingIndex(editorPalette.AvailableConstructData, gameEnvironment.ConstructDatas, constructDataAsInts);
            FillArrayWithMatchingIndex(
                editorPalette.AvailableConstructRepresentationProviders,
                gameEnvironment.ConstructRepresentationProviders,
                constructRepresentationAsInts
            );
            FillArrayWithMatchingIndex(editorPalette.AvailableUnitData, gameEnvironment.UnitDatas, unitDataAsInts);
            FillArrayWithMatchingIndex(
                editorPalette.AvailableUnitRepresentationProviders,
                gameEnvironment.UnitRepresentationProviders,
                unitRepresentationAsInts
            );

            return new GameEnvironmentJson(
                tileDataAsInts,
                tileRepresentationAsInts,
                constructDataAsInts,
                constructRepresentationAsInts,
                unitDataAsInts,
                unitRepresentationAsInts,
                _mapCharacteristicRepository.GetInnerRadius(),
                _mapCharacteristicRepository.GetMap2DArrayWidth(),
                _mapCharacteristicRepository.GetMap2dArrayHeight(),
                upAxis.x,
                upAxis.y,
                upAxis.z
            );
        }

        private static void FillArrayWithMatchingIndex<T>(AvailableSet<T> availableSet, IReadOnlyList<T> datas, IList<int> arrayToFill)
        {
            var availableSetData = availableSet.Set;
            for (var i = 0; i < availableSetData.Length; i++)
            {
                var index = Array.IndexOf(availableSetData, datas[i]);

                arrayToFill[i] = index;
            }
        }
    }
}