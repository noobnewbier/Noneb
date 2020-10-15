using Noneb.Core.Game.Common.Providers;
using Noneb.Core.InGameEditor.Data;
using UnityEngine;
using UnityUtils.Constants;

namespace Noneb.Ui.InGameEditor.SelectedEditorPalettes
{
    [CreateAssetMenu(
        fileName = nameof(SelectedEditorPaletteRepositoryProvider),
        menuName = MenuName.ScriptableRepository + nameof(SelectedEditorPaletteRepository)
    )]
    public class SelectedEditorPaletteRepositoryProvider : ScriptableObject, IObjectProvider<ISelectedEditorPaletteRepository>
    {
        [SerializeField] private EditorPalette editorPalette;

        private ISelectedEditorPaletteRepository _repository;

        public ISelectedEditorPaletteRepository Provide() =>
            _repository ?? (_repository = new SelectedEditorPaletteRepository(editorPalette));
    }
}