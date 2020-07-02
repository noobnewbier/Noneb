using UnityEditor;
using UnityEngine;

namespace Tiles
{
    public class TileRepresentation : MonoBehaviour
    {
        public Tile Tile { get; private set; }

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

            Handles.Label(transform.position,Tile.Coordinate.ToString());
        }
    }
}