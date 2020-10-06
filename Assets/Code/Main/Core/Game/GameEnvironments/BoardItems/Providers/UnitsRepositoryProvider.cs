﻿using System;
using Main.Core.Game.Common.Providers;
using Main.Core.Game.Units;
using UnityEngine;
using UnityUtils.Constants;

namespace Main.Core.Game.GameEnvironments.BoardItems.Providers
{
    [CreateAssetMenu(fileName = nameof(UnitsRepositoryProvider), menuName = MenuName.ScriptableRepository + "UnitsRepository")]
    public class UnitsRepositoryProvider : ScriptableObjectProvider<BoardItemsRepository<Unit>>
    {
        private readonly Lazy<BoardItemsRepository<Unit>>
            _lazyInstance = new Lazy<BoardItemsRepository<Unit>>(() => new BoardItemsRepository<Unit>());

        public override BoardItemsRepository<Unit> Provide() => _lazyInstance.Value;
    }
}