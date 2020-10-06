using Main.Core.Game.Strongholds;
using Main.Ui.Game.Common.Holders;
using UnityEditor;
using UnityEngine;

namespace Main.Ui.Game.Strongholds
{
    public class StrongholdHolder : BoardItemHolder<Stronghold>
    {
        private void OnDrawGizmosSelected()
        {
            if (Value == null)
            {
                return;
            }

            var style = new GUIStyle {normal = {textColor = Color.cyan}};
            Handles.Label(
                transform.position,
                Value.Data.Name,
                style
            );
        }
    }
}