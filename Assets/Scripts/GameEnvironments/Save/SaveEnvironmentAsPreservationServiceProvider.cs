using Common.Providers;
using InGameEditor.Repositories.SelectedEditorPalettes;
using Maps;
using UnityEngine;
using UnityEngine.Serialization;
using UnityUtils.Constants;

namespace GameEnvironments.Save
{
    [CreateAssetMenu(
        menuName = MenuName.ScriptableService + "SaveEnvironmentAsPreservationService",
        fileName = nameof(SaveEnvironmentAsPreservationServiceProvider)
    )]
    public class SaveEnvironmentAsPreservationServiceProvider : ScriptableObjectProvider<ISaveEnvironmentService>
    {
        [SerializeField] private MapConfigurationProvider mapConfigurationProvider;
        [SerializeField] private SelectedEditorPaletteRepositoryProvider editorPaletteRepositoryProvider;

        private ISaveEnvironmentService _saveEnvironmentService;

        private void OnEnable()
        {
            _saveEnvironmentService = new SaveEnvironmentAsPreservationService(
                editorPaletteRepositoryProvider.Provide()
            );
        }

        public override ISaveEnvironmentService Provide()
        {
            return _saveEnvironmentService;
        }
    }
}