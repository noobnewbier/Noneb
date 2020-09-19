using System;
using Common.Providers;
using Tiles.Holders;
using UnityEngine;
using UnityUtils.Constants;

namespace GameEnvironments.Common.Repositories.BoardItemsHolder.Providers
{
    [CreateAssetMenu(fileName = nameof(TilesHolderRepositoryProvider), menuName = MenuName.Providers + "TilesHolderRepository")]
    public class TilesHolderRepositoryProvider : ScriptableObjectProvider<BoardItemsHolderRepository<TileHolder>>
    {
        private readonly Lazy<BoardItemsHolderRepository<TileHolder>> _lazyInstance =
            new Lazy<BoardItemsHolderRepository<TileHolder>>(() => new BoardItemsHolderRepository<TileHolder>());

        public override BoardItemsHolderRepository<TileHolder> Provide()
        {
            return _lazyInstance.Value;
        }
    }
}