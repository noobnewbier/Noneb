using UnityEngine;

namespace Noneb.Core.InGameEditor.Data
{
    public class PaletteData<T>
    {
        public PaletteData(Sprite icon, string name, T data)
        {
            Icon = icon;
            Name = name;
            Data = data;
        }

        public T Data { get; }
        public Sprite Icon { get; }
        public string Name { get; }
    }
}