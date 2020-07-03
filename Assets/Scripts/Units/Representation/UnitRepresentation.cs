using UnityEngine;

namespace Units.Representation
{
    public class UnitRepresentation : MonoBehaviour
    {
        public Unit Unit { get; private set; }

        public void Initialize(Unit unit)
        {
            Unit = unit;
        }
    }
}