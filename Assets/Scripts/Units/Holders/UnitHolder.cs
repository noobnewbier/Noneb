using Common.Holders;
using UnityEditor;
using UnityEngine;

namespace Units.Holders
{
    public class UnitHolder : MonoBehaviour, IHolder<Unit>
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