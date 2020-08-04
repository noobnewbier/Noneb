using Common.Providers;
using UnityEngine;
using UnityUtils.Constants;

namespace GameEnvironments.Load.GameObjects
{
    [CreateAssetMenu(fileName = nameof(GameObjectLoadServiceProvider), menuName = MenuName.ScriptableService+"TileGameObjectLoadService")]
    public class GameObjectLoadServiceProvider : ScriptableObjectProvider<IGameObjectLoadService>
    {
        public override IGameObjectLoadService Provide()
        {
            return new GameObjectLoadService();
        }
    }
}