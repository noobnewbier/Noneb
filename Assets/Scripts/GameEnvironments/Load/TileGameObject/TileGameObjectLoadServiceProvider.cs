using Common.Providers;
using UnityEngine;
using UnityUtils.Constants;

namespace GameEnvironments.Load.TileGameObject
{
    [CreateAssetMenu(fileName = nameof(TileGameObjectLoadServiceProvider), menuName = MenuName.ScriptableService+"TileGameObjectLoadService")]
    public class TileGameObjectLoadServiceProvider : ScriptableObjectProvider<ITileGameObjectLoadService>
    {
        public override ITileGameObjectLoadService Provide()
        {
            return new TileGameObjectLoadService();
        }
    }
}