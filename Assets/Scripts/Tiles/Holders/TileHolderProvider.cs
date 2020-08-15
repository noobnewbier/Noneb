using Common.Providers;
using UnityEngine;
using UnityUtils.Constants;

namespace Tiles.Holders
{
    [CreateAssetMenu(menuName = MenuName.Providers + "TileHolder", fileName = nameof(TileHolderProvider))]
    public class TileHolderProvider : ScriptableGameObjectAndComponentProvider<TileHolder>
    {
    }
}