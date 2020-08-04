using Common.Holders;
using UnityEditor;
using UnityEngine;

namespace Units.Holders
{
    public class UnitHolder : MonoBehaviour, IBoardItemHolder<Unit>
    {
        public Unit Value { get; private set; }

        public void Initialize(Unit unit)
        {
            Value = unit;
        }

        private void OnDrawGizmosSelected()
        {
            if (Value == null)
            {
                return;
            }

            var style = new GUIStyle {normal = {textColor = Color.yellow}};
            Handles.Label(
                transform.position,
                $"{Value.Data.UnitName}",
                style
            );
        }
    }
}