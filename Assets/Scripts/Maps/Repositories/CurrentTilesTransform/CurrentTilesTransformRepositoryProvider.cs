using Common.Providers;
using GameEnvironments.Load.Obsolete.Tiles;
using UnityEngine;
using UnityEngine.Serialization;
using UnityUtils.Constants;

namespace Maps.Repositories.CurrentTilesTransform
{
    [CreateAssetMenu(
        fileName = nameof(CurrentTilesTransformRepositoryProvider),
        menuName = MenuName.ScriptableRepository + nameof(CurrentTilesTransformRepository)
    )]
    public class CurrentTilesTransformRepositoryProvider : ScriptableObjectProvider<CurrentTilesTransformRepository>
    {
        [FormerlySerializedAs("mapLoadServiceProvider")] [SerializeField]
        private TileLoadServiceProvider tileLoadServiceProvider;

        private CurrentTilesTransformRepository _cache;

        public override CurrentTilesTransformRepository Provide()
        {
            return _cache ?? (_cache = new CurrentTilesTransformRepository(tileLoadServiceProvider.Provide()));
        }

        private void OnDisable()
        {
            _cache?.Dispose();
        }
    }
}