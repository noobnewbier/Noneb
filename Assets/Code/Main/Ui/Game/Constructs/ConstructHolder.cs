using Common.Holders;
using UnityEditor;
using UnityEngine;

namespace Constructs
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