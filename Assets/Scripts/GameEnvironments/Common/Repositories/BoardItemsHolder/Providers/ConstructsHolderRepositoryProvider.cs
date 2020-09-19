using System;
using Common.Providers;
using Constructs;
using UnityEngine;
using UnityUtils.Constants;

namespace GameEnvironments.Common.Repositories.BoardItemsHolder.Providers
{
    [CreateAssetMenu(fileName = nameof(ConstructsHolderRepositoryProvider), menuName = MenuName.Providers + "ConstructsHolderRepository")]
    public class ConstructsHolderRepositoryProvider : ScriptableObjectProvider<BoardItemsHolderRepository<ConstructHolder>>
    {
        private readonly Lazy<BoardItemsHolderRepository<ConstructHolder>> _lazyInstance =
            new Lazy<BoardItemsHolderRepository<ConstructHolder>>(() => new BoardItemsHolderRepository<ConstructHolder>());

        public override BoardItemsHolderRepository<ConstructHolder> Provide()
        {
            return _lazyInstance.Value;
        }
    }
}