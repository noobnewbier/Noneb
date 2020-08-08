using System.Collections.Immutable;
using Common.Providers;
using GameEnvironments.Common.Data;
using GameEnvironments.Common.Data.GameEnvironments;
using GameEnvironments.Common.Repositories.LevelDatas;

namespace GameEnvironments.Load.GameObjects.Loaders
{
    public class StrongholdUnitGameObjectLoader : GameObjectLoader
    {
        protected override ImmutableArray<GameObjectProvider> GetGameObjectProvidersFromRepository(ILevelDataRepository levelDataRepository)
        {
            return levelDataRepository.StrongholdUnitGameObjectProviders;
        }
    }
}