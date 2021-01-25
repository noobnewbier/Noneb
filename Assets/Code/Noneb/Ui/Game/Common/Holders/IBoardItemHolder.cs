using Noneb.Core.Game.Common.BoardItems;
using UnityEngine;
using UnityUtils.Pooling;

namespace Noneb.Ui.Game.Common.Holders
{
    public interface IBoardItemHolder : IPoolable<GameObject>
    {
        Transform Transform { get; }
        BoardItem Value { get; }
    }

    public interface IBoardItemHolder<T> : IBoardItemHolder where T : BoardItem
    {
        new T Value { get; }
        void Initialize(T value);
    }
}