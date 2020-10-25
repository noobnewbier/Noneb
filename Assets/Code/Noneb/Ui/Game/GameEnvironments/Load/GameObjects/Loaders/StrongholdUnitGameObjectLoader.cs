using System;
using System.Collections.Generic;
using Noneb.Core.Game.Common.Constants;
using Noneb.Core.Game.Common.Factories;
using Noneb.Core.Game.GameState.CurrentLevelDatas;
using Noneb.Ui.Game.Common.Holders;
using Noneb.Ui.Game.GameEnvironments.BoardItemsHoldersFetchingService;
using Noneb.Ui.Game.GameEnvironments.BoardItemsHoldersFetchingService.Providers;
using UniRx;
using UnityEngine;
using UnityEngine.Serialization;

namespace Noneb.Ui.Game.GameEnvironments.Load.GameObjects.Loaders
{
    [CreateAssetMenu(fileName = nameof(StrongholdUnitGameObjectLoader), menuName = ProjectMenuName.Loader + nameof(StrongholdUnitGameObjectLoader))]
    public class StrongholdUnitGameObjectLoader : GameObjectLoader
    {
        [FormerlySerializedAs("repositoryProvider")] [SerializeField]
        private StrongholdHoldersFetchingServiceProvider provider;

        protected override IObservable<IReadOnlyList<GameObjectFactory>> GetGameObjectProvidersFromRepository(
            ILevelDataRepository levelDataRepository)
        {
            return levelDataRepository.GetMostRecent().Select(d => d.StrongholdUnitGameObjectProviders);
        }

        protected override IBoardItemHoldersFetchingService<IBoardItemHolder> GetBoardItemsHolderFetchingService() => provider.Provide();
    }
}