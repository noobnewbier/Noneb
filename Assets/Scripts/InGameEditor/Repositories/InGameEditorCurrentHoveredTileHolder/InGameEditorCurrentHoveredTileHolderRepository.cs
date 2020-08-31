using Common;
using Tiles.Holders;

namespace InGameEditor.Repositories.InGameEditorCurrentHoveredTileHolder
{
    public interface IInGameEditorCurrentHoveredTileHolderGetRepository : IDataGetRepository<TileHolder>
    {
    }

    public interface IInGameEditorCurrentHoveredTileHolderSetRepository : IDataSetRepository<TileHolder>
    {
    }

    public class InGameEditorCurrentHoveredTileHolderRepository : DataRepository<TileHolder>,
                                                                  IInGameEditorCurrentHoveredTileHolderSetRepository,
                                                                  IInGameEditorCurrentHoveredTileHolderGetRepository
    {
    }
}