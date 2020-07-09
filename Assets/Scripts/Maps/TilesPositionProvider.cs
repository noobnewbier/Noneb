using System.Collections.Generic;
using Common.Providers;
using UnityEngine;

namespace Maps
{
    //we probably want a way to "fix" our order of storage for tiles...
    public class TilesPositionProvider : MonoObjectProvider<IList<Vector3>>
    {
        [SerializeField] private MapConfiguration config;
        
        public override IList<Vector3> Provide()
        {
            var toReturn = new List<Vector3>();            
            var upDistance = config.OuterRadius * 1.5f;
            var sideDistance = config.InnerRadius * 2f;
    
            for (var i = 0; i < config.ZSize; i++)
            {
                var sideOffset = i % 2 * sideDistance / 2f;
                for (var j = 0; j < config.XSize; j++)
                {
                    toReturn.Add(new Vector3(
                        j * sideDistance + sideOffset,
                        transform.position.y,
                        i * upDistance
                    ));
                }
            }

            return toReturn;
        }
    }
}