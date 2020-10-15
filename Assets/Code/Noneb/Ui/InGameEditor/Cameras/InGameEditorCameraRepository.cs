using Noneb.Core.Game.Common;
using UnityEngine;

namespace Noneb.Ui.InGameEditor.Cameras
{
    public interface IInGameEditorCameraGetRepository : IDataGetRepository<Camera>
    {
    }

    public interface IInGameEditorCameraSetRepository : IDataSetRepository<Camera>
    {
    }

    public class InGameEditorCameraRepository : DataRepository<Camera>, IInGameEditorCameraGetRepository, IInGameEditorCameraSetRepository
    {
    }
}