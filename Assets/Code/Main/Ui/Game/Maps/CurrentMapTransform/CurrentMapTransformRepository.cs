using Main.Core.Game.Common;
using UnityEngine;

namespace Main.Ui.Game.Maps.CurrentMapTransform
{
    public interface ICurrentMapTransformGetRepository : IDataGetRepository<Transform>
    {
    }

    public class CurrentMapTransformRepository : DataRepository<Transform>, ICurrentMapTransformGetRepository
    {
    }
}