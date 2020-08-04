using Common.BoardItems;
using UnityEngine;

namespace Units
{
    [CreateAssetMenu(fileName = "UnitData", menuName = "Data/Unit")]
    public class UnitData : ScriptableObject, IBoardItemData
    {
        [SerializeField] private string unitName;
        [SerializeField] private float health;
        [SerializeField] private float maxHealth;

        public string UnitName => unitName;
        public float MaxHealth => maxHealth;
        public float Health => health;
    }
}