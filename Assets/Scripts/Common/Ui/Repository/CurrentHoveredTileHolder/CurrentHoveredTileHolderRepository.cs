using Tiles.Holders;

namespace Common.Ui.Repository.CurrentHoveredTileHolder
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