using System.Collections.Generic;
using Noneb.Core.Game.Common;
using UnityEngine;

namespace Noneb.Ui.Game.Common.Holders
{
    public abstract class BoardItemsHolderFetcher<THolder> : MonoBehaviour, IFetcher<IReadOnlyList<THolder>>
    {
        protected abstract string HolderTag { get; }

        public IReadOnlyList<THolder> Fetch()
        {
            var toReturn = new List<THolder>();
            for (var i = 0; i < transform.childCount; i++)
            {
                var child = transform.GetChild(i);
                if (child.CompareTag(HolderTag)) toReturn.Add(child.GetComponent<THolder>());
            }

            return toReturn;
        }
    }
}