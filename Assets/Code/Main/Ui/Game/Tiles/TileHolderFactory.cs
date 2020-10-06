using Main.Core.Game.Common.Factories;
using UnityEngine;
using UnityUtils.Constants;

namespace Main.Ui.Game.Tiles
{
    [CreateAssetMenu(menuName = MenuName.Providers + "TileHolder", fileName = nameof(TileHolderFactory))]
    public class TileHolderFactory : PooledMonoBehaviourFactory<TileHolder>
    {
    }
}