using Common;
using UnityEngine;

namespace InGameEditor.Repositories.InGameEditorCamera
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