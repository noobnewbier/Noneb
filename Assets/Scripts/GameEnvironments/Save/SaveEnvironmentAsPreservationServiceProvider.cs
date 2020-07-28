using Common.Providers;
using Maps.Repositories;
using UnityEngine;
using UnityUtils.Constants;

namespace GameEnvironments.Save
{
    [CreateAssetMenu(menuName = MenuName.ScriptableService+"SaveEnvironmentAsPreservationService", fileName = nameof(SaveEnvironmentAsPreservationServiceProvider))]
    public class SaveEnvironmentAsPreservationServiceProvider : ScriptableObjectProvider<ISaveEnvironmentAsPreservationService>
    {
        [SerializeField] private MapCharacteristicRepositoryProvider mapCharacteristicRepositoryProvider;

        private ISaveEnvironmentAsPreservationService _saveEnvironmentAsPreservationService;

        private void OnEnable()
        {
            _saveEnvironmentAsPreservationService = new SaveEnvironmentAsPreservationService(mapCharacteristicRepositoryProvider.Provide());
        }

        public override ISaveEnvironmentAsPreservationService Provide()
        {
            return _saveEnvironmentAsPreservationService;
        }
    }
}