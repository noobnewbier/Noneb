using Main.Core.Game.Common;
using Main.Ui.Game.Tiles;

namespace Main.Ui.Game.UiState.CurrentSelectedTileHolder
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