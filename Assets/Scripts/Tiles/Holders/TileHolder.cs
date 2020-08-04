using Common.Holders;
using UnityEditor;
using UnityEngine;

namespace Tiles.Holders
{
    public class TileHolder : MonoBehaviour, IBoardItemHolder<Tile>
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