using Common.Providers;
using Constructs;
using Maps;
using Tiles.Data;
using Units;
using UnityEngine;
using UnityEngine.Serialization;

namespace GameEnvironments.Common.Data
{
    [CreateAssetMenu(menuName = "Data/GameEnvironment", fileName = nameof(GameEnvironmentScriptable))]
    public class GameEnvironmentScriptable : ScriptableObject
    {
        [FormerlySerializedAs("_mapConfiguration")] [SerializeField]
        private MapConfiguration mapConfiguration;

        [FormerlySerializedAs("_tileDatas")] [SerializeField]
        private TileData[] tileDatas;

        [FormerlySerializedAs("_tileGameObjectProviders")] [SerializeField]
        private GameObjectProvider[] tileGameObjectProviders;

        [FormerlySerializedAs("_constructDatas")] [SerializeField]
        private ConstructData[] constructDatas;

        [FormerlySerializedAs("_constructGameObjectProviders")] [SerializeField]
        private GameObjectProvider[] constructGameObjectProviders;

        [FormerlySerializedAs("_unitDatas")] [SerializeField]
        private UnitData[] unitDatas;

        [FormerlySerializedAs("_unitGameObjectProviders")] [SerializeField]
        private GameObjectProvider[] unitGameObjectProviders;

        public MapConfiguration MapConfiguration => mapConfiguration;

        public TileData[] TileDatas => tileDatas;

        public GameObjectProvider[] TileGameObjectProviders => tileGameObjectProviders;

        public ConstructData[] ConstructDatas => constructDatas;

        public GameObjectProvider[] ConstructGameObjectProviders => constructGameObjectProviders;

        public UnitData[] UnitDatas => unitDatas;

        public GameObjectProvider[] UnitGameObjectProviders => unitGameObjectProviders;
    }
}