using Main.Core.Game.Common.Providers;
using Main.Core.InGameEditor.Data;
using UnityEngine;

namespace Main.Ui.InGameEditor.SelectedEditorPalettes
{
    [CreateAssetMenu(fileName = nameof(SelectedEditorPaletteRepositoryProvider), menuName = "ScriptableRepository/SelectedEditorPaletteRepository")]
    public class SelectedEditorPaletteRepositoryProvider : ScriptableObjectProvider<ISelectedEditorPaletteRepository>
    {
        [SerializeField] private EditorPalette editorPalette;

        private ISelectedEditorPaletteRepository _repository;

        public override ISelectedEditorPaletteRepository Provide() =>
            _repository ?? (_repository = new SelectedEditorPaletteRepository(editorPalette));
    }
}