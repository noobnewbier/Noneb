using Noneb.Core.InGameEditor.Common;
using UnityEngine;

namespace Noneb.Core.InGameEditor.Data
{
    public abstract class PaletteData : IInspectable
    {
    }

    public class PaletteData<T> : PaletteData
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