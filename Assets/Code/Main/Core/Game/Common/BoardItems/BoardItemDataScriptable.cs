using UnityEngine;

namespace Common.BoardItems
{
    public abstract class BoardItemDataScriptable : ScriptableObject
    {
        [SerializeField] private Sprite icon;
        protected Sprite Icon => icon;
    }
}