using Common.Holders;
using UnityEditor;
using UnityEngine;
using UnityUtils;

namespace Constructs
{
    public class ConstructHolder : PooledMonoBehaviour, IBoardItemHolder<Construct>
    {
        public Construct Value { get; private set; }

        public void Initialize(Construct construct)
        {
            Value = construct;
        }

        private void OnDrawGizmosSelected()
        {
            if (Value == null)
            {
                return;
            }

            var style = new GUIStyle {normal = {textColor = Color.red}};
            Handles.Label(
                transform.position,
                $"{Value.Data.DataName}",
                style
            );
        }
    }
}