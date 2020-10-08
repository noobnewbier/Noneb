using System;
using Main.Core.Game.Common.Providers;
using Main.Core.Game.Strongholds;
using UnityEngine;
using UnityUtils.Constants;

namespace Main.Core.Game.GameState.BoardItems.Providers
{
    [CreateAssetMenu(fileName = nameof(StrongholdsRepositoryProvider), menuName = MenuName.ScriptableRepository + "StrongholdsRepository")]
    public class StrongholdsRepositoryProvider : ScriptableObject, IObjectProvider<IBoardItemsRepository<Stronghold>>
    {
        private readonly Lazy<IBoardItemsRepository<Stronghold>> _lazyInstance =
            new Lazy<IBoardItemsRepository<Stronghold>>(() => new BoardItemsRepository<Stronghold>());

        public IBoardItemsRepository<Stronghold> Provide() => _lazyInstance.Value;
    }
}