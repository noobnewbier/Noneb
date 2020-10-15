using Noneb.Core.Game.Common;
using Noneb.Ui.Game.Tiles;

namespace Noneb.Ui.Game.UiState.CurrentSelectedTileHolder
{
    public interface ICurrentSelectedTileHolderGetRepository : IDataGetRepository<TileHolder>
    {
    }

    public interface ICurrentSelectedTileHolderSetRepository : IDataSetRepository<TileHolder>
    {
    }

    public class CurrentSelectedTileHolderRepository : DataRepository<TileHolder>,
                                                       ICurrentSelectedTileHolderGetRepository,
                                                       ICurrentSelectedTileHolderSetRepository
    {
    }
}