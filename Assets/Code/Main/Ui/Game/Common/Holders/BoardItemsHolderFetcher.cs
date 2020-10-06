using System.Collections.Generic;
using Main.Core.Game.Common.Providers;

namespace Main.Ui.Game.Common.Holders
{
    public abstract class BoardItemsHolderFetcher<THolder> : MonoObjectProvider<IReadOnlyList<THolder>>
    {
        protected abstract string HolderTag { get; }

        public override IReadOnlyList<THolder> Provide()
        {
            var toReturn = new List<THolder>();
            for (var i = 0; i < transform.childCount; i++)
            {
                var child = transform.GetChild(i);
                if (child.CompareTag(HolderTag))
                {
                    toReturn.Add(child.GetComponent<THolder>());
                }
            }

            return toReturn;
        }
    }
}