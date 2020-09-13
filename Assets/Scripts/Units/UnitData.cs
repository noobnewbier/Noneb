using Common.BoardItems;
using UnityEngine;

namespace Units
{
    [CreateAssetMenu(fileName = "UnitData", menuName = "Data/Unit")]
    public class UnitData : BoardItemData
    {
        [SerializeField] private string unitName;
        [SerializeField] private float health;
        [SerializeField] private float maxHealth;

        public float MaxHealth => maxHealth;
        public float Health => health;
        public override string DataName => unitName;
    }
}