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

namespace Main.Ui.Game.GameEnvironments.Load.GameObjects.Loaders
{
    [CreateAssetMenu(fileName = nameof(UnitGameObjectLoader), menuName = ProjectMenuName.Loader + nameof(UnitGameObjectLoader))]
    public class UnitGameObjectLoader : GameObjectLoader
    {
        [SerializeField] private UnitHoldersFetchingServiceProvider repositoryProvider;

        protected override IObservable<IReadOnlyList<GameObjectFactory>> GetGameObjectProvidersFromRepository(
            ICurrentLevelDataRepository currentLevelDataRepository)
        {
            return currentLevelDataRepository.GetMostRecent().Select(d => d.UnitGameObjectProviders);
        }

        protected override IBoardItemHoldersFetchingService<IBoardItemHolder> GetBoardItemsHolderFetchingService() => repositoryProvider.Provide();
    }
}