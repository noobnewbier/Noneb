using System;
using System.Linq;
using Common.Providers;
using Constructs;
using JetBrains.Annotations;
using Maps;
using Strongholds;
using Tiles.Data;
using Units;
using UnityEngine;

namespace GameEnvironments.Common.Data
{
    [CreateAssetMenu(menuName = "Data/GameEnvironment", fileName = nameof(GameEnvironmentScriptable))]
    public class GameEnvironmentScriptable : ScriptableObject
    {
        /// <summary>
        /// Required to avoid passing in empty but not null object into the environment
        /// </summary>
        [Serializable]
        private class StrongholdDataWrapper
        {
            [SerializeField] private UnitData unitData;
            [SerializeField] private ConstructData constructData;

            public StrongholdDataWrapper(UnitData unitData, ConstructData constructData)
            {
                this.unitData = unitData;
                this.constructData = constructData;
            }

            public StrongholdData ToStrongholdData()
            {
                if (unitData == null && constructData == null)
                {
                    return null;
                }
                
                return new StrongholdData(unitData, constructData);
            }
        }

        [SerializeField] private MapConfiguration mapConfiguration;
        [SerializeField] private TileData[] tileDatas;
        [SerializeField] private GameObjectProvider[] tileGameObjectProviders;
        [SerializeField] private ConstructData[] constructDatas;
        [SerializeField] private GameObjectProvider[] constructGameObjectProviders;
        [SerializeField] private UnitData[] unitDatas;
        [SerializeField] private GameObjectProvider[] unitGameObjectProviders;
        [SerializeField] private StrongholdDataWrapper[] strongholdDatas;
        [SerializeField] private GameObjectProvider[] strongholdUnitGameObjectProviders;
        [SerializeField] private GameObjectProvider[] strongholdConstructGameObjectProviders;

#if UNITY_EDITOR
        [TextArea] [SerializeField] [UsedImplicitly]
        private string devInformation;
#endif


        public GameEnvironment ToGameEnvironment()
        {
            return new GameEnvironment(
                mapConfiguration,
                tileDatas,
                tileGameObjectProviders,
                constructDatas,
                constructGameObjectProviders,
                unitDatas,
                unitGameObjectProviders,
                strongholdDatas.Select(wrapper => wrapper.ToStrongholdData()).ToArray(),
                strongholdUnitGameObjectProviders,
                strongholdConstructGameObjectProviders
            );
        }
    }
}