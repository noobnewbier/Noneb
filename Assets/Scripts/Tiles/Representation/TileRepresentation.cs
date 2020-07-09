using Common.Representations;
using UnityEditor;
using UnityEngine;

namespace Tiles.Representation
{
    public class TileRepresentation : MonoBehaviour, IRepresentation<Tile>
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