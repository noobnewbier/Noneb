using System;
using Noneb.Core.Game.Coordinates;
using Noneb.Core.Game.GameState.LevelDatas;
using Noneb.Core.Game.GameState.MapConfigs;
using Noneb.Core.InGameEditor.LevelDataEditing;
using UniRx;

namespace Noneb.Core.InGameEditor.EditorAction
{
    public static partial class EditorActions
    {
        public static class Stronghold
        {
            public static IObservable<Unit> SetUpStrongholdInLevelData(Coordinate coordinate,
                                                                       ILevelDataEditingService levelDataEditingService,
                                                                       IMapConfigRepository mapConfigRepository,
                                                                       ILevelDataRepository levelDataRepository) =>
                levelDataRepository.GetMostRecent()
                    .Zip(mapConfigRepository.GetMostRecent(), (data, config) => (data, config))
                    .SelectMany(tuple => levelDataEditingService.SetUpStronghold(tuple.data, tuple.config, coordinate));

            public static IObservable<Unit> DestructStrongholdInLevelData(Coordinate coordinate,
                                                                          ILevelDataEditingService levelDataEditingService,
                                                                          IMapConfigRepository mapConfigRepository,
                                                                          ILevelDataRepository levelDataRepository)
            {
                return levelDataRepository.GetMostRecent()
                    .Zip(mapConfigRepository.GetMostRecent(), (data, config) => (data, config))
                    .SelectMany(tuple => levelDataEditingService.DestructStronghold(tuple.data, tuple.config, coordinate));
            }
        }
    }
}