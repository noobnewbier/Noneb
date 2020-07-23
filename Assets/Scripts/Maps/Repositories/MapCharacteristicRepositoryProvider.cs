using Common.Providers;
using UnityEngine;

namespace Maps.Repositories
{
    [CreateAssetMenu(fileName = nameof(MapCharacteristicRepositoryProvider), menuName = "ScriptableRepository/MapCharacteristicRepository")]
    public class MapCharacteristicRepositoryProvider : ScriptableObjectProvider<IMapCharacteristicRepository>
    {
        [SerializeField] private MapConfiguration mapConfig;

        private IMapCharacteristicRepository _repository;

        private void OnEnable()
        {
            _repository = new MapCharacteristicRepository(mapConfig);
        }

        public override IMapCharacteristicRepository Provide()
        {
            return _repository;
        }
    }
}