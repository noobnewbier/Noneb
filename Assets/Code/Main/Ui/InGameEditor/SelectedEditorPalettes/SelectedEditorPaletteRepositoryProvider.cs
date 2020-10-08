using Main.Core.Game.Common.Providers;
using Main.Core.InGameEditor.Data;
using UnityEngine;
using UnityUtils.Constants;

namespace Main.Ui.InGameEditor.SelectedEditorPalettes
{
    [CreateAssetMenu(fileName = nameof(SelectedEditorPaletteRepositoryProvider), menuName = MenuName.ScriptableRepository + nameof(SelectedEditorPaletteRepository))]
    public class SelectedEditorPaletteRepositoryProvider : ScriptableObject, IObjectProvider<ISelectedEditorPaletteRepository>
    {
        [SerializeField] private EditorPalette editorPalette;

        private ISelectedEditorPaletteRepository _repository;

        public ISelectedEditorPaletteRepository Provide() =>
            _repository ?? (_repository = new SelectedEditorPaletteRepository(editorPalette));
    }
}