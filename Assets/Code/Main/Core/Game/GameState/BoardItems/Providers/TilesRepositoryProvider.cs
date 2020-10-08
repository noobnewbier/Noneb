using System;
using Main.Core.Game.Common.Providers;
using Main.Core.Game.Tiles;
using UnityEngine;
using UnityUtils.Constants;

namespace Main.Core.Game.GameState.BoardItems.Providers
{
    [CreateAssetMenu(fileName = nameof(TilesRepositoryProvider), menuName = MenuName.ScriptableRepository + "TilesRepository")]
    public class TilesRepositoryProvider : ScriptableObject, IObjectProvider<IBoardItemsRepository<Tile>>
    {
        private readonly Lazy<IBoardItemsRepository<Tile>>
            _lazyInstance = new Lazy<IBoardItemsRepository<Tile>>(() => new BoardItemsRepository<Tile>());

        public IBoardItemsRepository<Tile> Provide() => _lazyInstance.Value;
    }
}