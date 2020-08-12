using System.Collections.Immutable;
using Common.Providers;
using Constructs;
using GameEnvironments.Common.Data.LevelDatas;
using Strongholds;
using Tiles.Data;
using Units;

namespace GameEnvironments.Common.Repositories.CurrentLevelData
{
    public interface ICurrentLevelDataRepository
    {
        ImmutableArray<TileData> TileDatas { get; }
        ImmutableArray<GameObjectProvider> TileGameObjectProviders { get; }
        ImmutableArray<ConstructData> ConstructDatas { get; }
        ImmutableArray<GameObjectProvider> ConstructGameObjectProviders { get; }
        ImmutableArray<UnitData> UnitDatas { get; }
        ImmutableArray<GameObjectProvider> UnitGameObjectProviders { get; }
        ImmutableArray<StrongholdData> StrongholdDatas { get; }
        ImmutableArray<GameObjectProvider> StrongholdUnitGameObjectProviders { get; }
        ImmutableArray<GameObjectProvider> StrongholdConstructGameObjectProviders { get; }
    }

    public class CurrentLevelDataRepository : ICurrentLevelDataRepository
    {
        private readonly LevelData _levelData;

        public CurrentLevelDataRepository(LevelData levelData)
        {
            _levelData = levelData;
        }

        public ImmutableArray<TileData> TileDatas => _levelData.TileDatas.ToImmutableArray();
        public ImmutableArray<GameObjectProvider> TileGameObjectProviders => _levelData.TileGameObjectProviders.ToImmutableArray();
        public ImmutableArray<ConstructData> ConstructDatas => _levelData.ConstructDatas.ToImmutableArray();
        public ImmutableArray<GameObjectProvider> ConstructGameObjectProviders => _levelData.ConstructGameObjectProviders.ToImmutableArray();
        public ImmutableArray<UnitData> UnitDatas => _levelData.UnitDatas.ToImmutableArray();
        public ImmutableArray<GameObjectProvider> UnitGameObjectProviders => _levelData.UnitGameObjectProviders.ToImmutableArray();
        public ImmutableArray<StrongholdData> StrongholdDatas => _levelData.StrongholdDatas.ToImmutableArray();

        public ImmutableArray<GameObjectProvider> StrongholdUnitGameObjectProviders =>
            _levelData.StrongholdUnitGameObjectProviders.ToImmutableArray();

        public ImmutableArray<GameObjectProvider> StrongholdConstructGameObjectProviders =>
            _levelData.StrongholdConstructGameObjectProviders.ToImmutableArray();
    }
}