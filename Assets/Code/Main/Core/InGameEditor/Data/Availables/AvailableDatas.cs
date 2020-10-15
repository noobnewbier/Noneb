using UnityEngine;
using UnityEngine.Serialization;

namespace Main.Core.InGameEditor.Data.Availables
{
    public abstract class AvailableDatas<T> : ScriptableObject
    {
        public abstract PaletteData<T>[] Datas { get; }
    }
}