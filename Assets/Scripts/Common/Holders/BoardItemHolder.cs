using Common.BoardItems;
using UnityEngine;
using UnityUtils.Pooling;

namespace Common.Holders
{
    public abstract class BoardItemHolder<T> : PooledMonoBehaviour, IBoardItemHolder<T> where T : BoardItem
    {
        public Transform Transform => transform;
        public T Value { get; private set; }

        public virtual void Initialize(T value)
        {
            Value = value;
        }

        BoardItem IBoardItemHolder.Value => Value;
    }
}