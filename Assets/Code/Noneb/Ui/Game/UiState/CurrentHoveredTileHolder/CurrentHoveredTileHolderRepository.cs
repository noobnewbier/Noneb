using Noneb.Core.Game.Common;
using Noneb.Ui.Game.Tiles;

namespace Noneb.Ui.Game.UiState.CurrentHoveredTileHolder
{
    public interface ICurrentHoveredTileHolderGetRepository : IDataGetRepository<TileHolder>
    {
    }

    public interface ICurrentHoveredTileHolderSetRepository : IDataSetRepository<TileHolder>
    {
    }

    public class CurrentHoveredTileHolderRepository : DataRepository<TileHolder>,
                                                      ICurrentHoveredTileHolderSetRepository,
                                                      ICurrentHoveredTileHolderGetRepository
    {
    }
}