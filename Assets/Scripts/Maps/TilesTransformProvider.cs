using System.Collections.Generic;
using Common;
using UnityEngine;

namespace Maps
{
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
                        if (grandChild.CompareTag(ObjectTags.TileRepresentation))
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