using System;
using Main.Core.Game.Common.Providers;
using Main.Core.Game.Units;
using UnityEngine;
using UnityUtils.Constants;

namespace Main.Core.Game.GameState.BoardItems.Providers
{
    [CreateAssetMenu(fileName = nameof(UnitsRepositoryProvider), menuName = MenuName.ScriptableRepository + "UnitsRepository")]
    public class UnitsRepositoryProvider : ScriptableObject, IObjectProvider<IBoardItemsRepository<Unit>>
    {
        private readonly Lazy<IBoardItemsRepository<Unit>>
            _lazyInstance = new Lazy<IBoardItemsRepository<Unit>>(() => new BoardItemsRepository<Unit>());

        public IBoardItemsRepository<Unit> Provide() => _lazyInstance.Value;
    }
}