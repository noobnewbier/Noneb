using Common.BoardItems;
using UnityEngine;
using UnityUtils;
using UnityUtils.Pooling;

namespace Common.Holders
{
    //Do we ACTUALLY need this, in theory we can probably get away without it considering all the holders are doing is showing gizmos atm
    public interface IBoardItemHolder : IPoolable<GameObject>
    {
        Transform Transform { get; }
    }

    public interface IBoardItemHolder<T> : IBoardItemHolder where T : BoardItem
    {
        T Value { get; }
        void Initialize(T t);
    }
}