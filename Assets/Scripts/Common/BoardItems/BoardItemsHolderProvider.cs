﻿using System.Collections.Generic;
using Common.Providers;

namespace Common.BoardItems
{
    public abstract class BoardItemsHolderProvider<THolder> : MonoObjectProvider<IList<THolder>>
    {
        protected abstract string HolderTag { get; }

        public override IList<THolder> Provide()
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