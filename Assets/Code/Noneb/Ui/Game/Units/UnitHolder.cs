using Noneb.Core.Game.Units;
using Noneb.Ui.Game.Common.Holders;
using UnityEditor;
using UnityEngine;

namespace Noneb.Ui.Game.Units
{
    public class UnitHolder : BoardItemHolder<Unit>
    {
        private void OnDrawGizmosSelected()
        {
            if (Value == null) return;

            var style = new GUIStyle {normal = {textColor = Color.yellow}};
            Handles.Label(
                transform.position,
                $"{Value.Data.Name}",
                style
            );
        }
    }
}