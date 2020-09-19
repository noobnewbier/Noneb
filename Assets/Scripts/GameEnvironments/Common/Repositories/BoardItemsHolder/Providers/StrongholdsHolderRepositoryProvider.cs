using System;
using Common.Providers;
using Strongholds;
using UnityEngine;
using UnityUtils.Constants;

namespace GameEnvironments.Common.Repositories.BoardItemsHolder.Providers
{
    [CreateAssetMenu(fileName = nameof(StrongholdsHolderRepositoryProvider), menuName = MenuName.Providers + "StrongholdsHolderRepository")]
    public class StrongholdsHolderRepositoryProvider : ScriptableObjectProvider<BoardItemsHolderRepository<StrongholdHolder>>
    {
        private readonly Lazy<BoardItemsHolderRepository<StrongholdHolder>> _lazyInstance =
            new Lazy<BoardItemsHolderRepository<StrongholdHolder>>(() => new BoardItemsHolderRepository<StrongholdHolder>());

        public override BoardItemsHolderRepository<StrongholdHolder> Provide()
        {
            return _lazyInstance.Value;
        }
    }
}