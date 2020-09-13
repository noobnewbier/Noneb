using UnityEngine;

namespace Common.BoardItems
{
    public abstract class BoardItemData : ScriptableObject
    {
        [SerializeField] private Sprite icon;

        public Sprite Icon => icon;

        public abstract string DataName { get; }
    }
}