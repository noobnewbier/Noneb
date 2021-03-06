﻿using System;
using Noneb.Core.Game.Common.Providers;
using Noneb.Core.Game.Units;
using UnityEngine;
using UnityUtils.Constants;

namespace Noneb.Core.Game.GameState.BoardItems.Providers
{
    [CreateAssetMenu(fileName = nameof(UnitsRepositoryProvider), menuName = MenuName.ScriptableRepository + "UnitsRepository")]
    public class UnitsRepositoryProvider : ScriptableObject, IObjectProvider<IBoardItemsRepository<Unit>>
    {
        private readonly Lazy<IBoardItemsRepository<Unit>>
            _lazyInstance = new Lazy<IBoardItemsRepository<Unit>>(() => new BoardItemsRepository<Unit>());

        public IBoardItemsRepository<Unit> Provide() => _lazyInstance.Value;
    }
}