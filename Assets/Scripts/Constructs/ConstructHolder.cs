using Common.BoardItems;
using Common.Holders;
using UnityEditor;
using UnityEngine;
using UnityUtils.Pooling;

namespace Constructs
{
    public class ConstructHolder : PooledMonoBehaviour, IBoardItemHolder<Construct>
    {
        public Construct Value { get; private set; }

        public void Initialize(Construct construct)
        {
            Value = construct;
        }

        public Transform Transform => transform;
        BoardItem IBoardItemHolder.Value => Value;

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