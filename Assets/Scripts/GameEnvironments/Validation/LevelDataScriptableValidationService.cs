using System.Collections.Generic;
using System.Linq;
using Common.BoardItems;
using Common.Providers;
using GameEnvironments.Common.Data.LevelDatas;
using InGameEditor.Data;
using Maps;
using Tiles.Data;

namespace GameEnvironments.Validation
{
    //todo: actually use this shit :)
    public interface ILevelDataScriptableValidationService
    {
        bool Validate(LevelData levelData, MapConfig mapConfiguration, EditorPalette editorPalette);
    }

    public class LevelDataScriptableValidationService : ILevelDataScriptableValidationService
    {
        public bool Validate(LevelData levelData, MapConfig mapConfiguration, EditorPalette editorPalette)
        {
            return AllDataOfCorrectSize(levelData, mapConfiguration) &&
                   AllTileHasData(levelData.TileDatas, levelData.TileGameObjectProviders) &&
                   AllOnTileBoardDataHasCorrespondingGameObjectProviderAndViceVersa(levelData) &&
                   NoOtherOnTileBoardItemWhenThereIsStronghold(levelData);
        }

        private bool AllDataOfCorrectSize(LevelData levelData, MapConfig mapConfiguration)
        {
            var arraysSize = mapConfiguration.GetTotalMapSize();

            return levelData.TileDatas.Length == arraysSize &&
                   levelData.TileGameObjectProviders.Length == arraysSize &&
                   levelData.ConstructDatas.Length == arraysSize &&
                   levelData.ConstructGameObjectProviders.Length == arraysSize &&
                   levelData.UnitDatas.Length == arraysSize &&
                   levelData.UnitGameObjectProviders.Length == arraysSize &&
                   levelData.StrongholdDatas.Length == arraysSize &&
                   levelData.StrongholdUnitGameObjectProviders.Length == arraysSize &&
                   levelData.StrongholdConstructGameObjectProviders.Length == arraysSize;
        }

        private bool AllTileHasData(IEnumerable<TileData> tileDatas, IEnumerable<GameObjectProvider> tileGameObjectProviders)
        {
            return tileDatas.All(d => d != null) &&
                   tileGameObjectProviders.All(g => g != null);
        }

        private bool AllOnTileBoardDataHasCorrespondingGameObjectProviderAndViceVersa(LevelData levelData)
        {
            return DataHasCorrespondingGameObjectProvider(levelData.ConstructDatas, levelData.ConstructGameObjectProviders) &&
                   DataHasCorrespondingGameObjectProvider(levelData.UnitDatas, levelData.UnitGameObjectProviders) &&
                   DataHasCorrespondingGameObjectProvider(levelData.StrongholdDatas, levelData.StrongholdUnitGameObjectProviders) &&
                   DataHasCorrespondingGameObjectProvider(levelData.StrongholdDatas, levelData.StrongholdConstructGameObjectProviders);
        }

        private bool NoOtherOnTileBoardItemWhenThereIsStronghold(LevelData levelData)
        {
            var strongholdDatas = levelData.StrongholdDatas;
            var unitDatas = levelData.UnitDatas;
            var constructDatas = levelData.ConstructDatas;

            // ReSharper disable once LoopCanBeConvertedToQuery : for readability
            for (var i = 0; i < strongholdDatas.Length; i++)
                if (strongholdDatas[i] != null) // if there is a stronghold
                {
                    if (unitDatas[i] != null || constructDatas[i] != null) // there should be no unit or construct
                    {
                        return false;
                    }
                }

            return true;
        }

        private bool DataHasCorrespondingGameObjectProvider<T>(IReadOnlyList<T> boardItemDatas,
                                                               IReadOnlyList<GameObjectProvider> gameObjectProviders) where T : BoardItemData
        {
            // ReSharper disable once LoopCanBeConvertedToQuery : for readability
            for (var i = 0; i < boardItemDatas.Count; i++)
                if (boardItemDatas[i] != null && gameObjectProviders[i] == null || //have data but not corresponding gameObjectProvider
                    boardItemDatas[i] == null && gameObjectProviders[i] != null) //have gameObjectProvider but not corresponding data 
                {
                    return false;
                }

            return true;
        }
    }
}