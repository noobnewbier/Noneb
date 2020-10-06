﻿using Main.Core.InGameEditor.Data;

namespace Main.Ui.InGameEditor.SelectedEditorPalettes
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