using Common.Holders;
using UnityEditor;
using UnityEngine;

namespace Constructs
{
    public class ConstructHolder : MonoBehaviour, IHolder<Construct>
    {
        public Construct Construct { get; private set; }

        public void Initialize(Construct construct)
        {
            Construct = construct;
        }

        private void OnDrawGizmosSelected()
        {
            if (Construct == null)
            {
                return;
            }

            var style = new GUIStyle {normal = {textColor = Color.red}};
            Handles.Label(
                transform.position,
                $"{Construct.ConstructData.ConstructName}",
                style
            );
        }
    }
}