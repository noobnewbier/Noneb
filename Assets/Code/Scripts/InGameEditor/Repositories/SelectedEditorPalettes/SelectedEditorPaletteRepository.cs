using InGameEditor.Data;

namespace InGameEditor.Repositories.SelectedEditorPalettes
{
    public interface ISelectedEditorPaletteRepository
    {
        EditorPalette Palette { get; }
    }

    public class SelectedEditorPaletteRepository : ISelectedEditorPaletteRepository
    {
        public SelectedEditorPaletteRepository(EditorPalette palette)
        {
            Palette = palette;
        }

        public EditorPalette Palette { get; }
    }
}