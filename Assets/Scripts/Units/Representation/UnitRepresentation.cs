using Common.Representations;
using UnityEditor;
using UnityEngine;

namespace Units.Representation
{
    public class UnitRepresentation : MonoBehaviour, IRepresentation<Unit>
    {
        public Unit Unit { get; private set; }

        public void Initialize(Unit unit)
        {
            Unit = unit;
        }

        private void OnDrawGizmosSelected()
        {
            if (Unit == null)
            {
                return;
            }

            var style = new GUIStyle {normal = {textColor = Color.yellow}};
            Handles.Label(
                transform.position,
                $"{Unit.UnitData.UnitName}",
                style
            );
        }
    }
}