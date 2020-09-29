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

        protected override void OnReturnToPool()
        {
            base.OnReturnToPool();

            foreach (Transform child in transform)
                Destroy(child.gameObject);
        }

        BoardItem IBoardItemHolder.Value => Value;
    }
}