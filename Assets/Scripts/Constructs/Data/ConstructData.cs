using Common.BoardItems;
using UnityEngine;

namespace Constructs.Data
{
    public class ConstructData : BoardItemData
    {
        public ConstructDataScriptable Original { get; }

        public ConstructData(Sprite icon, string name, ConstructDataScriptable original) : base(icon, name)
        {
            Original = original;
        }
    }
}