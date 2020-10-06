﻿using System;
using Main.Core.Game.Common.Providers;
using Main.Ui.Game.Common.Holders;
using Main.Ui.Game.Units;
using UnityEngine;
using UnityUtils.Constants;

namespace Main.Ui.Game.GameEnvironments.BoardItemsFetcherRepository.Providers
{
    [CreateAssetMenu(
        fileName = nameof(UnitsHolderFetcherRepositoryProvider),
        menuName = MenuName.ScriptableRepository + "UnitsHolderFetcherRepository"
    )]
    public class UnitsHolderFetcherRepositoryProvider : ScriptableObjectProvider<
        BoardItemsHolderFetcherRepository<BoardItemsHolderFetcher<UnitHolder>, UnitHolder>>
    {
        private readonly Lazy<BoardItemsHolderFetcherRepository<BoardItemsHolderFetcher<UnitHolder>, UnitHolder>> _lazyInstance =
            new Lazy<BoardItemsHolderFetcherRepository<BoardItemsHolderFetcher<UnitHolder>, UnitHolder>>(
                () => new BoardItemsHolderFetcherRepository<BoardItemsHolderFetcher<UnitHolder>, UnitHolder>()
            );

        public override BoardItemsHolderFetcherRepository<BoardItemsHolderFetcher<UnitHolder>, UnitHolder> Provide() => _lazyInstance.Value;
    }
}