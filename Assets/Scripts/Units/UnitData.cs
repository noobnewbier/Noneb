using UnityEngine;

namespace Units
{
    [CreateAssetMenu(fileName = "UnitData", menuName = "Data/Unit")]
    public class UnitData : ScriptableObject
    {
        [SerializeField] private float health;
        [SerializeField] private float maxHealth;

        public float MaxHealth => maxHealth;
        public float Health => health;
    }
}