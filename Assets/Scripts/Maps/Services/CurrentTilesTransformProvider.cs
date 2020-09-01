using Common.Providers;
using GameEnvironments.Load.Tiles;
using UnityEngine;
using UnityUtils.Constants;

namespace Maps.Services
{
    [CreateAssetMenu(
        fileName = nameof(CurrentTilesTransformRepositoryProvider),
        menuName = MenuName.ScriptableRepository + nameof(CurrentTilesTransformRepository)
    )]
    public class CurrentTilesTransformRepositoryProvider : ScriptableObjectProvider<CurrentTilesTransformRepository>
    {
        [SerializeField] private MapLoadServiceProvider mapLoadServiceProvider;

        private CurrentTilesTransformRepository _cache;

        public override CurrentTilesTransformRepository Provide()
        {
            return _cache ?? (_cache = new CurrentTilesTransformRepository(mapLoadServiceProvider.Provide()));
        }

        private void OnDisable()
        {
            _cache?.Dispose();
        }
    }
}