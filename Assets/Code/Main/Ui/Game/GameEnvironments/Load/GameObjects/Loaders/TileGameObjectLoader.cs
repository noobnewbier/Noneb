using System;
using System.Collections.Generic;
using Main.Core.Game.Common.Constants;
using Main.Core.Game.Common.Factories;
using Main.Core.Game.GameState.CurrentLevelDatas;
using Main.Ui.Game.Common.Holders;
using Main.Ui.Game.GameEnvironments.BoardItemsHoldersFetchingService;
using Main.Ui.Game.GameEnvironments.BoardItemsHoldersFetchingService.Providers;
using UniRx;
using UnityEngine;

namespace Main.Ui.Game.GameEnvironments.Load.GameObjects.Loaders
{
    [CreateAssetMenu(fileName = nameof(TileGameObjectLoader), menuName = ProjectMenuName.Loader + nameof(TileGameObjectLoader))]
    public class TileGameObjectLoader : GameObjectLoader
    {
        [SerializeField] private TileHoldersFetchingServiceProvider repositoryProvider;

        protected override IObservable<IReadOnlyList<GameObjectFactory>> GetGameObjectProvidersFromRepository(
            ICurrentLevelDataRepository currentLevelDataRepository)
        {
            return currentLevelDataRepository.GetMostRecent().Select(d => d.TileGameObjectProviders);
        }

        protected override IBoardItemHoldersFetchingService<IBoardItemHolder> GetBoardItemsHolderFetchingService() => repositoryProvider.Provide();
    }
}