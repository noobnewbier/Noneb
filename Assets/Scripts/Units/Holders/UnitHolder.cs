using Common.Holders;
using UnityEditor;
using UnityEngine;
using UnityUtils;

namespace Units.Holders
{
    public class UnitHolder : PooledMonoBehaviour, IBoardItemHolder<Unit>
    {
        public Unit Value { get; private set; }

        public void Initialize(Unit unit)
        {
            Value = unit;
        }

        public Transform Transform => transform;

        private void OnDrawGizmosSelected()
        {
            if (Value == null)
            {
                return;
            }

            var style = new GUIStyle {normal = {textColor = Color.yellow}};
            Handles.Label(
                transform.position,
                $"{Value.Data.DataName}",
                style
            );
        }
    }
}