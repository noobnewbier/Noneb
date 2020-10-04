using Common.Holders;
using UnityEditor;
using UnityEngine;

namespace Strongholds
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