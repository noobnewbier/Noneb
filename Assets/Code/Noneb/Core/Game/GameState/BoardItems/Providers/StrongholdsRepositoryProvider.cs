using System;
using Noneb.Core.Game.Common.Providers;
using Noneb.Core.Game.Strongholds;
using UnityEngine;
using UnityUtils.Constants;

namespace Noneb.Core.Game.GameState.BoardItems.Providers
{
    [CreateAssetMenu(fileName = nameof(StrongholdsRepositoryProvider), menuName = MenuName.ScriptableRepository + "StrongholdsRepository")]
    public class StrongholdsRepositoryProvider : ScriptableObject, IObjectProvider<IBoardItemsRepository<Stronghold>>
    {
        private readonly Lazy<IBoardItemsRepository<Stronghold>> _lazyInstance =
            new Lazy<IBoardItemsRepository<Stronghold>>(() => new BoardItemsRepository<Stronghold>());

        public IBoardItemsRepository<Stronghold> Provide() => _lazyInstance.Value;
    }
}