using Main.Core.Game.Constructs;
using Main.Ui.Game.Common.Holders;
using UnityEditor;
using UnityEngine;

namespace Main.Ui.Game.Constructs
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