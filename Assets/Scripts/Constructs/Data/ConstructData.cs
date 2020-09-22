using Common.BoardItems;
using UnityEngine;

namespace Constructs.Data
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