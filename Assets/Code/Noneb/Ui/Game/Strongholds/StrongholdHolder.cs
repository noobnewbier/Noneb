using Noneb.Core.Game.Strongholds;
using Noneb.Ui.Game.Common.Holders;
using UnityEditor;
using UnityEngine;

namespace Noneb.Ui.Game.Strongholds
{
    public class StrongholdHolder : BoardItemHolder<Stronghold>
    {
        private void OnDrawGizmosSelected()
        {
            if (Value == null) return;

            var style = new GUIStyle {normal = {textColor = Color.cyan}};
            Handles.Label(
                transform.position,
                Value.Data.Name,
                style
            );
        }
    }
}