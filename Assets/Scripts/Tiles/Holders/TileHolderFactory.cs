using Common.Factories;
using UnityEngine;
using UnityUtils.Constants;

namespace Tiles.Holders
{
    [CreateAssetMenu(menuName = MenuName.Providers + "TileHolder", fileName = nameof(TileHolderFactory))]
    public class TileHolderFactory : PooledMonoBehaviourFactory<TileHolder>
    {
    }
}