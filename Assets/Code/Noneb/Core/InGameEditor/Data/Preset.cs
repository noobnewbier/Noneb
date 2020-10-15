using System;
using Noneb.Core.Game.Common.BoardItems;
using Noneb.Core.Game.Common.Factories;

namespace Noneb.Core.InGameEditor.Data
{
    [Serializable]
    public class Preset<T> where T : BoardItemData
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