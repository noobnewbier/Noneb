using System.Collections.Generic;
using Constructs;
using GameEnvironments.Common.Data;
using GameEnvironments.Common.Data.LevelDatas;
using InGameEditor.Data;
using InGameEditor.Data.Availables;
using InGameEditor.Repositories.SelectedEditorPalettes;
using Maps;
using Strongholds;
using Units;
using WorldConfigurations;

namespace ObsoleteJsonRelated
{
    public interface ICreateGameEnvironmentFromJsonService
    {
        GameEnvironment Create(GameEnvironmentJson gameEnvironmentJson);
    }

    public class CreateGameEnvironmentFromJsonService : ICreateGameEnvironmentFromJsonService
    {
        private readonly ISelectedEditorPaletteRepository _editorPaletteRepository;

        public CreateGameEnvironmentFromJsonService(ISelectedEditorPaletteRepository editorPaletteRepository)
        {
            _editorPaletteRepository = editorPaletteRepository;
        }

        public GameEnvironment Create(GameEnvironmentJson gameEnvironmentJson)
        {
            var palette = _editorPaletteRepository.Palette;
            var levelData = CreateLevelData(gameEnvironmentJson.LevelDataJson, palette);
            var mapConfig = CreateMapConfiguration(gameEnvironmentJson.MapConfigurationJson);
            var worldConfig = CreateWorldConfiguration(gameEnvironmentJson.WorldConfigurationJson);

            return new GameEnvironment(mapConfig, worldConfig, levelData, gameEnvironmentJson.EnvironmentName);
        }


        private LevelData CreateLevelData(LevelDataJson levelDataJson, EditorPalette palette)
        {
            return new LevelData(
                CreateArrayWithMatchingObject(palette.AvailableTileDatas, levelDataJson.TileDatas),
                CreateArrayWithMatchingObject(palette.AvailableTileGameObjectProviders, levelDataJson.TileGameObjectProviders),
                CreateArrayWithMatchingObject(palette.AvailableConstructDatas, levelDataJson.ConstructDatas),
                CreateArrayWithMatchingObject(palette.AvailableConstructGameObjectProviders, levelDataJson.ConstructGameObjectProviders),
                CreateArrayWithMatchingObject(palette.AvailableUnitDatas, levelDataJson.UnitDatas),
                CreateArrayWithMatchingObject(palette.AvailableUnitGameObjectProviders, levelDataJson.UnitGameObjectProviders),
                CreateStrongholdDatas(
                    palette.AvailableUnitDatas,
                    palette.AvailableConstructDatas,
                    levelDataJson.StrongholdUnitDatas,
                    levelDataJson.StrongholdConstructDatas
                ),
                CreateArrayWithMatchingObject(palette.AvailableUnitGameObjectProviders, levelDataJson.StrongholdUnitGameObjectProviders),
                CreateArrayWithMatchingObject(palette.AvailableConstructGameObjectProviders, levelDataJson.StrongholdConstructGameObjectProviders)
            );
        }

        private MapConfig CreateMapConfiguration(MapConfigurationJson mapConfigurationJson)
        {
            return MapConfig.Create(mapConfigurationJson.XSize, mapConfigurationJson.ZSize);
        }

        private WorldConfig CreateWorldConfiguration(WorldConfigurationJson worldConfigurationJson)
        {
            return WorldConfig.Create(worldConfigurationJson.UpAxis, worldConfigurationJson.InnerRadius);
        }

        private StrongholdData[] CreateStrongholdDatas(AvailableSet<UnitData> availableUnits,
                                                       AvailableSet<ConstructData> availableConstruct,
                                                       IReadOnlyList<int> strongholdUnitDataIndexes,
                                                       IReadOnlyList<int> strongholdConstructDataIndexes)
        {
            //we assume at this point, everything has been validated, and hence if there is a unit for stronghold, there MUST be a construct
            var dataLength = strongholdConstructDataIndexes.Count;
            var toReturn = new StrongholdData[dataLength];
            var availableUnitsSet = availableUnits.Set;
            var availableConstructSet = availableConstruct.Set;


            for (var i = 0; i < dataLength; i++)
                if (strongholdUnitDataIndexes[i] != -1)
                {
                    toReturn[i] = new StrongholdData(
                        availableUnitsSet[strongholdUnitDataIndexes[i]],
                        availableConstructSet[strongholdConstructDataIndexes[i]]
                    );
                }
            return toReturn;
        }

        private static T[] CreateArrayWithMatchingObject<T>(AvailableSet<T> availableSet, IReadOnlyList<int> indexes)
        {
            var toReturn = new T[indexes.Count];
            var availableSetData = availableSet.Set;
            for (var i = 0; i < toReturn.Length; i++) toReturn[i] = availableSetData[indexes[i]];

            return toReturn;
        }
    }
}