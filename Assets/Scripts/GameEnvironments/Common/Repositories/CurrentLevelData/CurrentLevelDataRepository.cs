using System.Collections.Immutable;
using Common.Providers;
using Constructs;
using GameEnvironments.Common.Data.LevelDatas;
using GameEnvironments.Common.Repositories.CurrentGameEnvironment;
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
        private readonly ICurrentGameEnvironmentRepository _currentGameEnvironmentRepository;

        public CurrentLevelDataRepository(ICurrentGameEnvironmentRepository currentGameEnvironmentRepository)
        {
            _currentGameEnvironmentRepository = currentGameEnvironmentRepository;
        }

        public ImmutableArray<TileData> TileDatas => _currentGameEnvironmentRepository.Get().LevelData.TileDatas.ToImmutableArray();
        public ImmutableArray<GameObjectProvider> TileGameObjectProviders => _currentGameEnvironmentRepository.Get().LevelData.TileGameObjectProviders.ToImmutableArray();
        public ImmutableArray<ConstructData> ConstructDatas => _currentGameEnvironmentRepository.Get().LevelData.ConstructDatas.ToImmutableArray();
        public ImmutableArray<GameObjectProvider> ConstructGameObjectProviders => _currentGameEnvironmentRepository.Get().LevelData.ConstructGameObjectProviders.ToImmutableArray();
        public ImmutableArray<UnitData> UnitDatas => _currentGameEnvironmentRepository.Get().LevelData.UnitDatas.ToImmutableArray();
        public ImmutableArray<GameObjectProvider> UnitGameObjectProviders => _currentGameEnvironmentRepository.Get().LevelData.UnitGameObjectProviders.ToImmutableArray();
        public ImmutableArray<StrongholdData> StrongholdDatas => _currentGameEnvironmentRepository.Get().LevelData.StrongholdDatas.ToImmutableArray();

        public ImmutableArray<GameObjectProvider> StrongholdUnitGameObjectProviders =>
            _currentGameEnvironmentRepository.Get().LevelData.StrongholdUnitGameObjectProviders.ToImmutableArray();

        public ImmutableArray<GameObjectProvider> StrongholdConstructGameObjectProviders =>
            _currentGameEnvironmentRepository.Get().LevelData.StrongholdConstructGameObjectProviders.ToImmutableArray();
    }
}