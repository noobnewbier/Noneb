using Common.Providers;
using InGameEditor.Data;
using UnityEngine;

namespace InGameEditor.Repositories.SelectedEditorPalettes
{
    [CreateAssetMenu(fileName = nameof(SelectedEditorPaletteRepositoryProvider), menuName = "ScriptableRepository/SelectedEditorPaletteRepository")]
    public class SelectedEditorPaletteRepositoryProvider : ScriptableObjectProvider<ISelectedEditorPaletteRepository>
    {
        [SerializeField] private EditorPalette editorPalette;

        private ISelectedEditorPaletteRepository _repository;

        private void OnEnable()
        {
            _repository = new SelectedEditorPaletteRepository(editorPalette);
        }

        public override ISelectedEditorPaletteRepository Provide()
        {
            return _repository;
        }
    }
}