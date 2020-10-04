using System;
using Common.Providers;
using Tiles;
using UnityEngine;
using UnityUtils.Constants;

namespace GameEnvironments.Common.Repositories.BoardItems.Providers
{
    [CreateAssetMenu(fileName = nameof(TilesRepositoryProvider), menuName = MenuName.ScriptableRepository + "TilesRepository")]
    public class TilesRepositoryProvider : ScriptableObjectProvider<BoardItemsRepository<Tile>>
    {
        private readonly Lazy<BoardItemsRepository<Tile>>
            _lazyInstance = new Lazy<BoardItemsRepository<Tile>>(() => new BoardItemsRepository<Tile>());

        public override BoardItemsRepository<Tile> Provide() => _lazyInstance.Value;
    }
}