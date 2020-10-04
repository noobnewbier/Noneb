using Common;
using UnityEngine;

namespace Maps.Repositories.CurrentMapTransform
{
    public interface ICurrentMapTransformGetRepository : IDataGetRepository<Transform>
    {
    }

    public class CurrentMapTransformRepository : DataRepository<Transform>, ICurrentMapTransformGetRepository
    {
    }
}