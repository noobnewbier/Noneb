using Common.Holders;
using UnityEditor;
using UnityEngine;

namespace Tiles.Holders
{
    public class TileHolder : MonoBehaviour, IHolder<Tile>
    {
        [SerializeField] private Tile tile;

        public Tile Tile
        {
            get => tile;
            private set => tile = value;
        }

        public void Initialize(Tile t)
        {
            Tile = t;

            gameObject.name = "Tile: " + Tile.Coordinate;
        }

        private void OnDrawGizmosSelected()
        {
            if (Tile == null)
            {
                return;
            }

            Handles.Label(transform.position, Tile.Coordinate.ToString());
        }
    }
}