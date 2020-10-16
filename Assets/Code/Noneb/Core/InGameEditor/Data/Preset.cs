using System;
using Noneb.Core.Game.Common.BoardItems;
using Noneb.Core.Game.Common.Factories;
using Noneb.Core.InGameEditor.Common;

namespace Noneb.Core.InGameEditor.Data
{
    [Serializable]
    public class Preset<T> : IInspectable where T : BoardItemData
    {
        public T Data { get; }
        public GameObjectFactory GameObjectFactory { get; }

        public Preset(T data, GameObjectFactory gameObjectFactory)
        {
            Data = data;
            GameObjectFactory = gameObjectFactory;
        }
    }
}