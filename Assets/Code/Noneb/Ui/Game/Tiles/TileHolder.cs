using Noneb.Core.Game.Tiles;
using Noneb.Ui.Game.Common.Holders;
using UnityEditor;

namespace Noneb.Ui.Game.Tiles
{
    public class TileHolder : BoardItemHolder<Tile>
    {
        public override void Initialize(Tile value)
        {
            base.Initialize(value);
            gameObject.name = "Tile: " + Value.Coordinate;
        }

        private void OnDrawGizmosSelected()
        {
            if (Value == null) return;

            Handles.Label(transform.position, Value.Coordinate.ToString());
        }
    }
}