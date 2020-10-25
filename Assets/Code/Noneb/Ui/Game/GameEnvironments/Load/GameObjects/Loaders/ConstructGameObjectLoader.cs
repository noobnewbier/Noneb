using System;
using System.Collections.Generic;
using Noneb.Core.Game.Common.Constants;
using Noneb.Core.Game.Common.Factories;
using Noneb.Core.Game.GameState.LevelDatas;
using Noneb.Ui.Game.Common.Holders;
using Noneb.Ui.Game.GameEnvironments.BoardItemsHoldersFetchingService;
using Noneb.Ui.Game.GameEnvironments.BoardItemsHoldersFetchingService.Providers;
using UniRx;
using UnityEngine;

namespace Noneb.Ui.Game.GameEnvironments.Load.GameObjects.Loaders
{
    [CreateAssetMenu(fileName = nameof(ConstructGameObjectLoader), menuName = ProjectMenuName.Loader + nameof(ConstructGameObjectLoader))]
    public class ConstructGameObjectLoader : GameObjectLoader
    {
        [SerializeField] private ConstructsHoldersFetchingServiceProvider repositoryProvider;

        protected override IObservable<IReadOnlyList<GameObjectFactory>> GetGameObjectProvidersFromRepository(
            ILevelDataRepository levelDataRepository)
        {
            return levelDataRepository.GetMostRecent().Select(d => d.ConstructGameObjectProviders);
        }

        protected override IBoardItemHoldersFetchingService<IBoardItemHolder> GetBoardItemsHolderFetchingService() => repositoryProvider.Provide();
    }
}