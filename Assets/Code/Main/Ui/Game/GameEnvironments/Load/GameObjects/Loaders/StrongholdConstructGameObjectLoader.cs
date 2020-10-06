﻿using System;
using System.Collections.Generic;
using Main.Core.Game.Common.Constants;
using Main.Core.Game.Common.Factories;
using Main.Core.Game.GameEnvironments.CurrentLevelDatas;
using Main.Ui.Game.Common.Holders;
using Main.Ui.Game.GameEnvironments.BoardItemsHoldersFetchingService;
using Main.Ui.Game.GameEnvironments.BoardItemsHoldersFetchingService.Providers;
using UniRx;
using UnityEngine;
using UnityEngine.Serialization;

namespace Main.Ui.Game.GameEnvironments.Load.GameObjects.Loaders
{
    [CreateAssetMenu(
        fileName = nameof(StrongholdConstructGameObjectLoader),
        menuName = ProjectMenuName.Loader + nameof(StrongholdConstructGameObjectLoader)
    )]
    public class StrongholdConstructGameObjectLoader : GameObjectLoader
    {
        [FormerlySerializedAs("repositoryProvider")] [SerializeField] private StrongholdHoldersFetchingServiceProvider provider;

        protected override IObservable<IReadOnlyList<GameObjectFactory>> GetGameObjectProvidersFromRepository(
            ICurrentLevelDataRepository currentLevelDataRepository)
        {
            return currentLevelDataRepository.GetMostRecent().Select(d => d.StrongholdConstructGameObjectProviders);
        }

        protected override IBoardItemHoldersFetchingService<IBoardItemHolder> GetBoardItemsHolderFetchingService() => provider.Provide();
    }
}