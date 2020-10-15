using Noneb.Core.Game.Constructs;
using Noneb.Ui.Game.Common.Holders;
using UnityEditor;
using UnityEngine;

namespace Noneb.Ui.Game.Constructs
{
    public class ConstructHolder : BoardItemHolder<Construct>
    {
        private void OnDrawGizmosSelected()
        {
            if (Value == null)
            {
                return;
            }

            var style = new GUIStyle {normal = {textColor = Color.red}};
            Handles.Label(
                transform.position,
                $"{Value.Data.Name}",
                style
            );
        }
    }
}