using System;
using System.Collections.Generic;
using Common;
using Common.Factories;
using Common.Holders;
using GameEnvironments.Common.Repositories.BoardItemsHolders;
using GameEnvironments.Common.Repositories.BoardItemsHolders.Providers;
using GameEnvironments.Common.Repositories.CurrentLevelDatas;
using UniRx;
using UnityEngine;
using UnityEngine.Serialization;

namespace GameEnvironments.Load.GameObjects.Loaders
{
    [CreateAssetMenu(fileName = nameof(StrongholdUnitGameObjectLoader), menuName = ProjectMenuName.Loader + nameof(StrongholdUnitGameObjectLoader))]
    public class StrongholdUnitGameObjectLoader : GameObjectLoader
    {
        [FormerlySerializedAs("repositoryProvider")] [SerializeField] private StrongholdHoldersFetchingServiceProvider provider;

        protected override IObservable<IReadOnlyList<GameObjectFactory>> GetGameObjectProvidersFromRepository(
            ICurrentLevelDataRepository currentLevelDataRepository)
        {
            return currentLevelDataRepository.GetMostRecent().Select(d => d.StrongholdUnitGameObjectProviders);
        }

        protected override IBoardItemHoldersFetchingService<IBoardItemHolder> GetBoardItemsHolderFetchingService() => provider.Provide();
    }
}