using System;
using Main.Core.Game.Common.Providers;
using Main.Core.Game.Strongholds;
using UnityEngine;
using UnityUtils.Constants;

namespace Main.Core.Game.GameEnvironments.BoardItems.Providers
{
    [CreateAssetMenu(fileName = nameof(StrongholdsRepositoryProvider), menuName = MenuName.ScriptableRepository + "StrongholdsRepository")]
    public class StrongholdsRepositoryProvider : ScriptableObjectProvider<BoardItemsRepository<Stronghold>>
    {
        private readonly Lazy<BoardItemsRepository<Stronghold>> _lazyInstance =
            new Lazy<BoardItemsRepository<Stronghold>>(() => new BoardItemsRepository<Stronghold>());

        public override BoardItemsRepository<Stronghold> Provide() => _lazyInstance.Value;
    }
}