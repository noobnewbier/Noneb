using Common.Holders;
using UnityEditor;
using UnityEngine;

namespace Units.Holders
{
    public class UnitHolder : BoardItemHolder<Unit>
    {
        private void OnDrawGizmosSelected()
        {
            if (Value == null)
            {
                return;
            }

            var style = new GUIStyle {normal = {textColor = Color.yellow}};
            Handles.Label(
                transform.position,
                $"{Value.Data.Name}",
                style
            );
        }
    }
}