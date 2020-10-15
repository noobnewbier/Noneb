using System;
using Noneb.Core.Game.Common.Providers;
using Noneb.Core.Game.Tiles;
using UnityEngine;
using UnityUtils.Constants;

namespace Noneb.Core.Game.GameState.BoardItems.Providers
{
    [CreateAssetMenu(fileName = nameof(TilesRepositoryProvider), menuName = MenuName.ScriptableRepository + "TilesRepository")]
    public class TilesRepositoryProvider : ScriptableObject, IObjectProvider<IBoardItemsRepository<Tile>>
    {
        private readonly Lazy<IBoardItemsRepository<Tile>>
            _lazyInstance = new Lazy<IBoardItemsRepository<Tile>>(() => new BoardItemsRepository<Tile>());

        public IBoardItemsRepository<Tile> Provide() => _lazyInstance.Value;
    }
}