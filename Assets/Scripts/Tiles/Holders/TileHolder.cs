using Common.Holders;
using UnityEditor;
using UnityEngine;
using UnityUtils.Pooling;

namespace Tiles.Holders
{
    public class TileHolder : PooledMonoBehaviour, IBoardItemHolder<Tile>
    {
        //serialize to show stuffs in the inspector.
        [SerializeField] private Tile tile;

        public Tile Value
        {
            get => tile;
            private set => tile = value;
        }

        public void Initialize(Tile t)
        {
            Value = t;

            gameObject.name = "Tile: " + Value.Coordinate;
        }

        public Transform Transform => transform;

        private void OnDrawGizmosSelected()
        {
            if (Value == null)
            {
                return;
            }

            Handles.Label(transform.position, Value.Coordinate.ToString());
        }
    }
}