using Constructs;
using Tiles.Holders;
using Units.Holders;
using UnityEngine;
using UnityEngine.Serialization;
using WorldConfigurations;

namespace Maps.Create
{
    public class MapGenerator : MonoBehaviour
    {
        [Range(0f, 1f)] [SerializeField] private float chanceOfConstructOnTile;
        [Range(0f, 1f)] [SerializeField] private float chanceOfUnitOnTile;
        [FormerlySerializedAs("config")] [SerializeField] private MapConfiguration mapConfig;
        [SerializeField] private WorldConfigurationProvider worldConfigurationProvider;
        
        [FormerlySerializedAs("constructRepresentationProvider")] [SerializeField] private ConstructHolderProvider constructHolderProvider;
        [FormerlySerializedAs("tileRepresentationProvider")] [SerializeField] private TileHolderProvider tileHolderProvider;
        [SerializeField] private TilesPositionProvider tilesPositionProvider;
        [FormerlySerializedAs("unitHoldersProvider")] [FormerlySerializedAs("unitRepresentationProvider")] [SerializeField] private UnitHolderProvider unitHolderProvider;
        [SerializeField] private GameObject rowPrefab;


        [ContextMenu("GenerateMap")]
        private void GenerateMap()
        {
            var selfTransform = transform;
            var positions = tilesPositionProvider.Provide();
            var worldConfig = worldConfigurationProvider.Provide();

            for (var i = 0; i < mapConfig.ZSize; i++)
            {
                var row = Instantiate(rowPrefab).transform;
                row.parent = selfTransform;
                for (var j = 0; j < mapConfig.XSize; j++)
                {
                    var newTile = tileHolderProvider.Provide().GameObject.transform;

                    newTile.parent = row;
                    newTile.rotation *= Quaternion.AngleAxis(30f, worldConfig.UpAxis);
                    newTile.position = positions[i * mapConfig.XSize + j];
                }
            }
        }
    }
}