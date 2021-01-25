using System;
using Noneb.Core.Game.Coordinates;
using Noneb.Core.Game.GameEnvironments.Data.LevelDatas;
using Noneb.Core.Game.Maps;
using Noneb.Core.Game.Strongholds;
using UniRx;

namespace Noneb.Core.InGameEditor.LevelDataEditing
{
    public interface ILevelDataEditingService
    {
        IObservable<Unit> SetUpStronghold(LevelData levelData, MapConfig mapConfig, Coordinate coordinate);
        IObservable<Unit> DestructStronghold(LevelData levelData, MapConfig mapConfig, Coordinate coordinate);
        IObservable<Unit> ChangeTilePreset(LevelData levelData, MapConfig mapConfig, Coordinate coordinate);
    }

    public class LevelDataEditingService : ILevelDataEditingService
    {
        private readonly ICoordinateService _coordinateService;

        public LevelDataEditingService(ICoordinateService coordinateService)
        {
            _coordinateService = coordinateService;
        }

        public IObservable<Unit> DestructStronghold(LevelData levelData, MapConfig mapConfig, Coordinate coordinate)
        {
            return Observable.Create<Unit>(
                observer =>
                {
                    var index = _coordinateService.GetFlattenArrayIndexFromAxialCoordinate(coordinate.X, coordinate.Z, mapConfig);

                    CreateUnitFromStrongholdInIndex(levelData, index);
                    CreateConstructFromStrongholdInIndex(levelData, index);
                    ClearStrongholdInIndex(levelData, index);

                    observer.OnNext(Unit.Default);
                    observer.OnCompleted();

                    return Disposable.Empty;
                }
            );
        }

        public IObservable<Unit> ChangeTilePreset(LevelData levelData, MapConfig mapConfig, Coordinate coordinate)
        {
            return Observable.Create<Unit>(
                observer =>
                {
                    var index = _coordinateService.GetFlattenArrayIndexFromAxialCoordinate(coordinate.X, coordinate.Z, mapConfig);
//todo: actual implementation
                    return Disposable.Empty;
                }
            );
        }

        private static void ClearStrongholdInIndex(LevelData levelData, int index)
        {
            levelData.StrongholdDatas[index] = null;
            levelData.StrongholdUnitGameObjectFactories[index] = null;
            levelData.StrongholdConstructGameObjectFactories[index] = null;
        }

        private static void CreateConstructFromStrongholdInIndex(LevelData levelData, int index)
        {
            levelData.ConstructDatas[index] = levelData.StrongholdDatas[index].ConstructData;
            levelData.ConstructGameObjectFactories[index] = levelData.StrongholdConstructGameObjectFactories[index];
        }

        private static void CreateUnitFromStrongholdInIndex(LevelData levelData, int index)
        {
            levelData.UnitDatas[index] = levelData.StrongholdDatas[index].UnitData;
            levelData.UnitGameObjectFactories[index] = levelData.StrongholdUnitGameObjectFactories[index];
        }

        #region SetUpStronghold

        public IObservable<Unit> SetUpStronghold(LevelData levelData, MapConfig mapConfig, Coordinate coordinate)
        {
            return Observable.Create<Unit>(
                observer =>
                {
                    var index = _coordinateService.GetFlattenArrayIndexFromAxialCoordinate(coordinate.X, coordinate.Z, mapConfig);

                    CreateStrongholdInIndex(levelData, index);
                    ClearUnitAndConstructInIndex(levelData, index);

                    observer.OnNext(Unit.Default);
                    observer.OnCompleted();

                    return Disposable.Empty;
                }
            );
        }

        private static void ClearUnitAndConstructInIndex(LevelData levelData, int index)
        {
            levelData.UnitDatas[index] = null;
            levelData.ConstructDatas[index] = null;
            levelData.UnitGameObjectFactories[index] = null;
            levelData.ConstructGameObjectFactories[index] = null;
        }

        private static void CreateStrongholdInIndex(LevelData levelData, int index)
        {
            levelData.StrongholdDatas[index] = StrongholdData.Create(levelData.ConstructDatas[index], levelData.UnitDatas[index]);
            levelData.StrongholdConstructGameObjectFactories[index] = levelData.ConstructGameObjectFactories[index];
            levelData.StrongholdUnitGameObjectFactories[index] = levelData.UnitGameObjectFactories[index];
        }

        #endregion
    }
}