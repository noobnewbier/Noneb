using Common;
using Tiles.Holders;

namespace InGameEditor.Repositories.InGameEditorCurrentHoveredTileHolder
{
    public interface IInGameEditorCurrentlyHoveredTileHolderGetRepository : IDataGetRepository<TileHolder>
    {
    }

    public interface IInGameEditorCurrentlyHoveredTileHolderSetRepository : IDataSetRepository<TileHolder>
    {
    }

    public class InGameEditorCurrentlyHoveredTileHolderRepository : DataRepository<TileHolder>,
                                                                    IInGameEditorCurrentlyHoveredTileHolderSetRepository,
                                                                    IInGameEditorCurrentlyHoveredTileHolderGetRepository
    {
    }
}