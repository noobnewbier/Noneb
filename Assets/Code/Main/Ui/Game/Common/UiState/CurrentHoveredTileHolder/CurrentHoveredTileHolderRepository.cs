using Main.Core.Game.Common;
using Main.Ui.Game.Tiles;

namespace Main.Ui.Game.Common.UiState.CurrentHoveredTileHolder
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