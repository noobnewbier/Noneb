using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Noneb.Core.Game.Common.Factories;
using Noneb.Core.Game.Constructs;
using Noneb.Core.Game.GameEnvironments.Data.LevelDatas;
using Noneb.Core.Game.Maps;
using Noneb.Core.Game.Tiles;
using Noneb.Core.Game.Units;

namespace Noneb.Core.Game.GameEnvironments.Validation
{
    public struct ValidationResult
    {
        public ValidationResult(IEnumerable<string> failedReason, bool isValid)
        {
            FailedReason = failedReason;
            IsValid = isValid;
        }

        public IEnumerable<string> FailedReason { get; }
        public bool IsValid { get; }
    }

    public interface ILevelDataScriptableValidationService
    {
        ValidationResult Validate(LevelDataScriptable levelData, MapConfig mapConfiguration);
    }

    public class LevelDataScriptableValidationService : ILevelDataScriptableValidationService
    {
        public ValidationResult Validate(LevelDataScriptable levelData, MapConfig mapConfiguration)
        {
            var result = SummarizeValidationResult(
                AllDataOfCorrectSize(levelData, mapConfiguration),
                AllTileHasData(levelData.TileDatas, levelData.TileGameObjectFactories),
                UnitAndConstructHasDataWhenHasGameObjectFactoryAndViceVersa(levelData),
                IsStrongHoldSetUpCorrectly(levelData),
                NoOtherOnTileBoardItemWhenThereIsStronghold(levelData)
            );

            return result;
        }

        private static ValidationResult SummarizeValidationResult(params ValidationResult[] testResults)
        {
            var result = new ValidationResult(
                testResults.SelectMany(r => r.FailedReason).ToArray(),
                testResults.All(r => r.IsValid)
            );
            return result;
        }

        #region UnitAndConstructHasDataWhenHasGameObjectProviderAndViceVersa

        private ValidationResult UnitAndConstructHasDataWhenHasGameObjectFactoryAndViceVersa(LevelDataScriptable levelData) =>
            SummarizeValidationResult(
                DataHasCorrespondingGameObjectProvider(levelData.ConstructDatas, levelData.ConstructGameObjectFactories, nameof(Construct)),
                DataHasCorrespondingGameObjectProvider(levelData.UnitDatas, levelData.UnitGameObjectFactories, nameof(Unit))
            );

        #endregion

        private ValidationResult DataHasCorrespondingGameObjectProvider(IList datas,
                                                                        IReadOnlyList<GameObjectFactory> gameObjectProviders,
                                                                        string datasName)
        {
            var passedTest = true;
            var failedReason = new List<string>();
            // ReSharper disable once LoopCanBeConvertedToQuery : for readability
            for (var i = 0; i < datas.Count; i++)
            {
                if (datas[i] != null && gameObjectProviders[i] == null)
                {
                    passedTest = false;
                    failedReason.Add($"{datasName} has data in index ${i}, but does not have a {nameof(GameObjectFactory)} for it");
                }

                if (datas[i] == null && gameObjectProviders[i] != null)
                {
                    passedTest = false;
                    failedReason.Add($"{datasName} has {nameof(GameObjectFactory)} in index ${i}, but does not have a data for it");
                }
            }

            return new ValidationResult(failedReason, passedTest);
        }

        #region NoOtherOnTileBoardItemWhenThereIsStronghold

        private ValidationResult NoOtherOnTileBoardItemWhenThereIsStronghold(LevelDataScriptable levelData)
        {
            var strongholdDatas = levelData.StrongholdDatas;
            var unitDatas = levelData.UnitDatas;
            var constructDatas = levelData.ConstructDatas;
            var passedTest = true;
            var failedReason = new List<string>();

            for (var i = 0; i < strongholdDatas.Length; i++)
                if (StrongholdWrapperHasAStronghold(strongholdDatas[i]))
                {
                    if (unitDatas[i] != null)
                    {
                        passedTest = false;
                        failedReason.Add($"There is an unit and a stronghold in index {i}");
                    }
                    if (constructDatas[i] != null)
                    {
                        passedTest = false;
                        failedReason.Add($"There is an construct and a stronghold in index {i}");
                    }
                }

            return new ValidationResult(failedReason, passedTest);
        }

        #endregion

        private bool StrongholdWrapperHasAStronghold(LevelDataScriptable.StrongholdDataWrapper dataWrapper) =>
            dataWrapper.UnitDataScriptable != null || dataWrapper.ConstructDataScriptable != null;

        #region AllDataOfCorrectSize

        private ValidationResult AllDataOfCorrectSize(LevelDataScriptable levelData, MapConfig mapConfiguration)
        {
            var arraysSize = mapConfiguration.GetTotalMapSize();

            var testTileDatasResult = CheckDataIsOfCorrectSize(
                levelData.TileDatas,
                arraysSize,
                nameof(levelData.TileDatas)
            );

            var testTileGameObjectProvidersResult = CheckDataIsOfCorrectSize(
                levelData.TileGameObjectFactories,
                arraysSize,
                nameof(levelData.TileGameObjectFactories)
            );

            var testConstructDatasResult = CheckDataIsOfCorrectSize(
                levelData.ConstructDatas,
                arraysSize,
                nameof(levelData.ConstructDatas)
            );

            var testConstructGameObjectProvidersResult = CheckDataIsOfCorrectSize(
                levelData.ConstructGameObjectFactories,
                arraysSize,
                nameof(levelData.ConstructGameObjectFactories)
            );

            var testUnitDatasResult = CheckDataIsOfCorrectSize(
                levelData.UnitDatas,
                arraysSize,
                nameof(levelData.UnitDatas)
            );

            var testUnitGameObjectProvidersResult = CheckDataIsOfCorrectSize(
                levelData.UnitGameObjectFactories,
                arraysSize,
                nameof(levelData.UnitGameObjectFactories)
            );

            var testStrongholdDatasResult = CheckDataIsOfCorrectSize(
                levelData.StrongholdDatas,
                arraysSize,
                nameof(levelData.StrongholdDatas)
            );

            var testStrongholdUnitGameObjectProvidersResult = CheckDataIsOfCorrectSize(
                levelData.StrongholdUnitGameObjectFactories,
                arraysSize,
                nameof(levelData.StrongholdUnitGameObjectFactories)
            );

            var testStrongholdConstructGameObjectProvidersResult = CheckDataIsOfCorrectSize(
                levelData.StrongholdConstructGameObjectFactories,
                arraysSize,
                nameof(levelData.StrongholdConstructGameObjectFactories)
            );

            var summaries = SummarizeValidationResult(
                testTileDatasResult,
                testTileGameObjectProvidersResult,
                testConstructDatasResult,
                testConstructGameObjectProvidersResult,
                testUnitDatasResult,
                testUnitGameObjectProvidersResult,
                testStrongholdDatasResult,
                testStrongholdUnitGameObjectProvidersResult,
                testStrongholdConstructGameObjectProvidersResult
            );

            return summaries;
        }

        private static ValidationResult CheckDataIsOfCorrectSize(ICollection datas,
                                                                 int targetSize,
                                                                 string datasName)
        {
            var passedTest = true;
            var failedReason = new List<string>();
            if (datas.Count != targetSize)
            {
                passedTest = false;
                failedReason.Add($"{datasName} size is {datas.Count}, the map size is {targetSize} ");
            }


            return new ValidationResult(failedReason, passedTest);
        }

        #endregion

        #region AllTileHasData

        private ValidationResult AllTileHasData(IReadOnlyList<TileDataScriptable> tileDatas,
                                                IReadOnlyList<GameObjectFactory> tileGameObjectProviders) =>
            SummarizeValidationResult(
                CheckAllDataIsNotNull(tileDatas, nameof(tileDatas)),
                CheckAllDataIsNotNull(tileGameObjectProviders, nameof(tileGameObjectProviders))
            );

        private ValidationResult CheckAllDataIsNotNull<T>(IReadOnlyList<T> datas, string datasName)
        {
            var passedTest = true;
            var failedReason = new List<string>();

            for (var i = 0; i < datas.Count; i++)
                if (datas[i] == null)
                {
                    passedTest = false;
                    failedReason.Add($"{datasName} has null data in index {i}, it cannot be null");
                }

            return new ValidationResult(failedReason, passedTest);
        }

        #endregion

        #region IsStrongHoldSetUpCorrectly

        private ValidationResult IsStrongHoldSetUpCorrectly(LevelDataScriptable levelData)
        {
            var dataWrappers = levelData.StrongholdDatas;

            var dataWrapperHasValidData = StrongholdDataWrapperEitherHasBothUnitAndConstructOrNone(dataWrappers);
            var onlyHasUnitGoWhenHasUnit = DataHasCorrespondingGameObjectProvider(
                dataWrappers.Select(d => d.UnitDataScriptable).ToList(),
                levelData.StrongholdUnitGameObjectFactories,
                nameof(levelData.StrongholdUnitGameObjectFactories)
            );
            var onlyHasConstructGoWhenHasConstruct = DataHasCorrespondingGameObjectProvider(
                dataWrappers.Select(d => d.ConstructDataScriptable).ToList(),
                levelData.StrongholdConstructGameObjectFactories,
                nameof(levelData.StrongholdConstructGameObjectFactories)
            );

            return SummarizeValidationResult(
                dataWrapperHasValidData,
                onlyHasUnitGoWhenHasUnit,
                onlyHasConstructGoWhenHasConstruct
            );
        }

        private ValidationResult StrongholdDataWrapperEitherHasBothUnitAndConstructOrNone(
            IReadOnlyList<LevelDataScriptable.StrongholdDataWrapper> dataWrappers
        )
        {
            var passedTest = true;
            var failedReason = new List<string>();

            for (var i = 0; i < dataWrappers.Count; i++)
            {
                var dataWrapper = dataWrappers[i];
                if (dataWrapper.UnitDataScriptable == null && dataWrapper.ConstructDataScriptable != null ||
                    dataWrapper.UnitDataScriptable != null && dataWrapper.ConstructDataScriptable == null)
                {
                    passedTest = false;
                    failedReason.Add($"{nameof(LevelDataScriptable.StrongholdDataWrapper)} in index {i} is inconsistent");
                }
            }

            return new ValidationResult(failedReason, passedTest);
        }

        #endregion
    }
}