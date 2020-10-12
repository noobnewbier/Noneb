using System.Linq;
using Main.Core.Game.Common.Factories;
using Main.Core.Game.Constructs;
using Main.Core.Game.GameEnvironments.Data.LevelDatas;
using Main.Core.Game.Maps;
using Main.Core.Game.Strongholds;
using Main.Core.Game.Tiles;
using Main.Core.Game.Units;
using UnityEngine;
using UnityUtils;

namespace Core.Game.Tests.GameEnvironments.Validation
{
    public static class LevelDataTestExtensions
    {
        public static LevelData GetEmptyLevelData(MapConfig config)
        {
            var mapSize = config.GetTotalMapSize();
            var tileDatas = Enumerable.Repeat<TileData>(
                    null,
                    mapSize
                )
                .ToArray();
            var constructDatas = Enumerable.Repeat<ConstructData>(
                    null,
                    mapSize
                )
                .ToArray();

            var unitDatas = Enumerable.Repeat<UnitData>(
                    null,
                    mapSize
                )
                .ToArray();

            var strongholdDatas = Enumerable.Repeat<StrongholdData>(
                    null,
                    mapSize
                )
                .ToArray();

            GameObjectFactory[] GetEmptyGoFactories()
            {
                return Enumerable.Repeat<GameObjectFactory>(
                        null,
                        mapSize
                    )
                    .ToArray();
            }


            return new LevelData(
                tileDatas,
                GetEmptyGoFactories(),
                constructDatas,
                GetEmptyGoFactories(),
                unitDatas,
                GetEmptyGoFactories(),
                strongholdDatas,
                new GameObjectFactory[mapSize],
                new GameObjectFactory[mapSize]
            );
        }

        public static LevelDataScriptable ToScriptable(this LevelData data) => LevelDataScriptable.Create(data);

        public static LevelData FillWithTiles(this LevelData data)
        {
            data.TileDatas.Fill(new TileData(null, string.Empty, ScriptableObject.CreateInstance<TileDataScriptable>()));
            data.TileGameObjectProviders.Fill(ScriptableObject.CreateInstance<GameObjectFactory>());

            return data;
        }

        public static LevelData FillWithUnits(this LevelData data)
        {
            data.UnitDatas.Fill(new UnitData(null, string.Empty, ScriptableObject.CreateInstance<UnitDataScriptable>()));
            data.UnitGameObjectProviders.Fill(ScriptableObject.CreateInstance<GameObjectFactory>());

            return data;
        }

        public static LevelData FillWithConstructs(this LevelData data)
        {
            data.ConstructDatas.Fill(new ConstructData(null, string.Empty, ScriptableObject.CreateInstance<ConstructDataScriptable>()));
            data.ConstructGameObjectProviders.Fill(ScriptableObject.CreateInstance<GameObjectFactory>());

            return data;
        }

        public static LevelData FillWithStrongholds(this LevelData data)
        {
            data.StrongholdDatas.Fill(
                StrongholdData.Create(
                    new ConstructData(null, string.Empty, ScriptableObject.CreateInstance<ConstructDataScriptable>()),
                    new UnitData(null, string.Empty, ScriptableObject.CreateInstance<UnitDataScriptable>())
                )
            );

            data.StrongholdConstructGameObjectProviders.Fill(ScriptableObject.CreateInstance<GameObjectFactory>());
            data.StrongholdUnitGameObjectProviders.Fill(ScriptableObject.CreateInstance<GameObjectFactory>());

            return data;
        }
    }
}