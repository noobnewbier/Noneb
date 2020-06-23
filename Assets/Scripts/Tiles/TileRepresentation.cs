using UnityEditor;
using UnityEngine;

namespace Tiles
{
    public class TileRepresentation : MonoBehaviour
    {
        [SerializeField] private Tile tile;

        public void Initialize(Tile t)
        {
            tile = t;

            gameObject.name = "Tile: " + tile.Coordinate;
        }


        private void OnDrawGizmosSelected()
        {
            if (tile == null)
            {
                return;
            }

            Handles.Label(transform.position,tile.Coordinate.ToString());
        }
    }
}