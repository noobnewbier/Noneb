using Main.Core.Game.Common;
using UnityEngine;

namespace Main.Ui.InGameEditor.Cameras
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