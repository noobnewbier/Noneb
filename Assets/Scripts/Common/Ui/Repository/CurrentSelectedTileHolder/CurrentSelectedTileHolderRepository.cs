using Tiles.Holders;

namespace Common.Ui.Repository.CurrentSelectedTileHolder
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