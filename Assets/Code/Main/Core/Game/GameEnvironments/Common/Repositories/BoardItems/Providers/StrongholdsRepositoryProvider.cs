using System;
using Common.Providers;
using Strongholds;
using UnityEngine;
using UnityUtils.Constants;

namespace GameEnvironments.Common.Repositories.BoardItems.Providers
{
    [CreateAssetMenu(fileName = nameof(StrongholdsRepositoryProvider), menuName = MenuName.ScriptableRepository + "StrongholdsRepository")]
    public class StrongholdsRepositoryProvider : ScriptableObjectProvider<BoardItemsRepository<Stronghold>>
    {
        private readonly Lazy<BoardItemsRepository<Stronghold>> _lazyInstance =
            new Lazy<BoardItemsRepository<Stronghold>>(() => new BoardItemsRepository<Stronghold>());

        public override BoardItemsRepository<Stronghold> Provide() => _lazyInstance.Value;
    }
}