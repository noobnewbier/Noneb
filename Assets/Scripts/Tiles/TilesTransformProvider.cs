﻿using System.Collections.Generic;
using Common.Constants;
using Common.Providers;
using UnityEngine;

namespace Tiles
{
    //this seems weird and bad... IList<Transform> does not implies it to be a tile transform.
    public class TilesTransformProvider : MonoObjectProvider<IList<Transform>>
    {
        public override IList<Transform> Provide()
        {
            var toReturn = new List<Transform>();
            for (var i = 0; i < transform.childCount; i++)
            {
                //do our best to ensure it is in order
                var child = transform.GetChild(i);
                if (child.CompareTag(ObjectTags.GridRow))
                {
                    for (var j = 0; j < child.childCount; j++)
                    {
                        var grandChild = child.GetChild(j);
                        if (grandChild.CompareTag(ObjectTags.TileHolder))
                        {
                            toReturn.Add(grandChild);
                        }
                    }
                }
            }

            return toReturn;
        }
    }
}