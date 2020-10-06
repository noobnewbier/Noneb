using System;
using Main.Core.Game.Common.Providers;
using Main.Core.Game.Tiles;
using UnityEngine;
using UnityUtils.Constants;

namespace Main.Core.Game.GameEnvironments.BoardItems.Providers
{
    [CreateAssetMenu(fileName = nameof(TilesRepositoryProvider), menuName = MenuName.ScriptableRepository + "TilesRepository")]
    public class TilesRepositoryProvider : ScriptableObjectProvider<BoardItemsRepository<Tile>>
    {
        private readonly Lazy<BoardItemsRepository<Tile>>
            _lazyInstance = new Lazy<BoardItemsRepository<Tile>>(() => new BoardItemsRepository<Tile>());

        public override BoardItemsRepository<Tile> Provide() => _lazyInstance.Value;
    }
}