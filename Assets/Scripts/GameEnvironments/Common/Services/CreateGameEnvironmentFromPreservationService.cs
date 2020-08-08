using System;
using System.Collections.Generic;
using GameEnvironments.Common.Data;
using InGameEditor.Data;
using InGameEditor.Data.Availables;
using Maps;
using UnityEngine;

namespace GameEnvironments.Common.Services
{
    public class CreateGameEnvironmentFromPreservationService
    {
        // public GameEnvironment CreateGamEnvironment(GameEnvironmentJson environmentJson, EditorPalette editorPalette)
        // {
        //     var config = MapConfiguration.Create(
        //         environmentJson.InnerRadius,
        //         environmentJson.XSize,
        //         environmentJson.ZSize,
        //         environmentJson.UpAxis
        //     );
        //     var arrayLength = _mapCharacteristicRepository.GetFlattenMapArrayLength();
        //     var upAxis = _mapCharacteristicRepository.GetUpAxis();
        //
        //     var tileDataAsInts = new int[arrayLength];
        //     var tileGameObjectAsInts = new int[arrayLength];
        //     var constructDataAsInts = new int[arrayLength];
        //     var constructGameObjectAsInts = new int[arrayLength];
        //     var unitDataAsInts = new int[arrayLength];
        //     var unitGameObjectAsInts = new int[arrayLength];
        //
        //     FillArrayWithMatchingProvider(editorPalette.AvailableTileData, gameEnvironment.TileDatas, tileDataAsInts);
        //     FillArrayWithMatchingProvider(
        //         editorPalette.AvailableTileGameObjectProviders,
        //         gameEnvironment.TileGameObjectProviders,
        //         tileGameObjectAsInts
        //     );
        //     FillArrayWithMatchingProvider(editorPalette.AvailableConstructData, gameEnvironment.ConstructDatas, constructDataAsInts);
        //     FillArrayWithMatchingProvider(
        //         editorPalette.AvailableConstructGameObjectProviders,
        //         gameEnvironment.ConstructGameObjectProviders,
        //         constructGameObjectAsInts
        //     );
        //     FillArrayWithMatchingProvider(editorPalette.AvailableUnitData, gameEnvironment.UnitDatas, unitDataAsInts);
        //     FillArrayWithMatchingProvider(
        //         editorPalette.AvailableUnitGameObjectProviders,
        //         gameEnvironment.UnitGameObjectProviders,
        //         unitGameObjectAsInts
        //     );
        //
        //     return new GameEnvironment(
        //         tileDataAsInts,
        //         tileGameObjectAsInts,
        //         constructDataAsInts,
        //         constructGameObjectAsInts,
        //         unitDataAsInts,
        //         unitGameObjectAsInts,
        //         _mapCharacteristicRepository.GetInnerRadius(),
        //         _mapCharacteristicRepository.GetMap2DArrayWidth(),
        //         _mapCharacteristicRepository.GetMap2DArrayHeight(),
        //         upAxis.x,
        //         upAxis.y,
        //         upAxis.z
        //     );
        // }
        //
        // private static void FillArrayWithMatchingProvider<T>(AvailableSet<T> availableSet, IReadOnlyList<int> indexes, IList<T> arrayToFill)
        // {
        //     var availableSetData = availableSet.Set;
        //     for (var i = 0; i < availableSetData.Length; i++)
        //     {
        //         arrayToFill[i] = availableSetData[indexes[i]];
        //     }
        // }
    }
}