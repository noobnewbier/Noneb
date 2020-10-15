using Noneb.Core.Game.Common;
using UnityEngine;

namespace Noneb.Ui.Game.UiState.CurrentMapTransform
{
    public interface ICurrentMapTransformGetRepository : IDataGetRepository<Transform>
    {
    }

    public class CurrentMapTransformRepository : DataRepository<Transform>, ICurrentMapTransformGetRepository
    {
    }
}