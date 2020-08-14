using System.Collections.Generic;
using Common.Providers;
using Maps.Repositories;
using UnityEngine;
using WorldConfigurations;
using WorldConfigurations.Repositories;

namespace Maps
{
    //we probably want a way to "fix" our order of storage for tiles...
    public class TilesPositionProvider : MonoObjectProvider<IList<Vector3>>
    {
        [SerializeField] private MapConfigurationRepositoryProvider mapConfigurationRepositoryProvider;
        [SerializeField] private WorldConfigurationRepositoryProvider worldConfigurationRepositoryProvider;


        public override IList<Vector3> Provide()
        {
            var toReturn = new List<Vector3>();
            var worldConfig = worldConfigurationRepositoryProvider.Provide().Get();
            var mapConfig = mapConfigurationRepositoryProvider.Provide().Get();
            var upDistance = worldConfig.OuterRadius * 1.5f;
            var sideDistance = worldConfig.InnerRadius * 2f;

            for (var i = 0; i < mapConfig.ZSize; i++)
            {
                var sideOffset = i % 2 * sideDistance / 2f;
                for (var j = 0; j < mapConfig.XSize; j++)
                    toReturn.Add(
                        new Vector3(
                            j * sideDistance + sideOffset,
                            transform.position.y,
                            i * upDistance
                        )
                    );
            }

            return toReturn;
        }
    }
}