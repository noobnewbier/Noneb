using Main.Core.Game.Common.BoardItems;
using UnityEngine;

namespace Main.Core.Game.Constructs.Data
{
    public class ConstructData : BoardItemData
    {
        public ConstructData(Sprite icon, string name, ConstructDataScriptable original) : base(icon, name)
        {
            Original = original;
        }

        public ConstructDataScriptable Original { get; }
    }
}