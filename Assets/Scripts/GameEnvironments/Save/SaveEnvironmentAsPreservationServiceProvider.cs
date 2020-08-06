using Common.Providers;
using InGameEditor.Repositories.SelectedEditorPalettes;
using Maps.Repositories;
using UnityEngine;
using UnityUtils.Constants;

namespace GameEnvironments.Save
{
    [CreateAssetMenu(
        menuName = MenuName.ScriptableService + "SaveEnvironmentAsPreservationService",
        fileName = nameof(SaveEnvironmentAsPreservationServiceProvider)
    )]
    public class SaveEnvironmentAsPreservationServiceProvider : ScriptableObjectProvider<ISaveEnvironmentService>
    {
        [SerializeField] private MapCharacteristicRepositoryProvider mapCharacteristicRepositoryProvider;
        [SerializeField] private SelectedEditorPaletteRepositoryProvider editorPaletteRepositoryProvider;
        
        private ISaveEnvironmentService _saveEnvironmentService;

        private void OnEnable()
        {
            _saveEnvironmentService = new SaveEnvironmentAsPreservationService(
                mapCharacteristicRepositoryProvider.Provide(),
                editorPaletteRepositoryProvider.Provide()
            );
        }

        public override ISaveEnvironmentService Provide()
        {
            return _saveEnvironmentService;
        }
    }
}