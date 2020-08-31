using Common;
using Tiles.Holders;

namespace InGameEditor.Repositories.InGameEditorCurrentSelectedTileHolder
{
    public interface IInGameEditorCurrentSelectedTileHolderGetRepository : IDataGetRepository<TileHolder>
    {
    }

    public interface IInGameEditorCurrentSelectedTileHolderSetRepository : IDataSetRepository<TileHolder>
    {
    }

    public class InGameEditorCurrentSelectedTileHolderRepository : DataRepository<TileHolder>,
                                                                   IInGameEditorCurrentSelectedTileHolderGetRepository,
                                                                   IInGameEditorCurrentSelectedTileHolderSetRepository
    {
    }
}