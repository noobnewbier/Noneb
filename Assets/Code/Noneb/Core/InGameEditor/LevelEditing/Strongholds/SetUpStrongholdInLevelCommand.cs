using System;
using Noneb.Core.Game.Commands;
using Noneb.Core.Game.Common.GameAction;
using Noneb.Core.Game.Coordinates;
using Noneb.Core.Game.GameState.LevelDatas;
using Noneb.Core.Game.GameState.MapConfigs;
using Noneb.Core.Game.GameState.Maps;
using Noneb.Core.Game.Maps.MapModification;
using Noneb.Core.InGameEditor.EditorAction;
using Noneb.Core.InGameEditor.LevelDataEditing;
using UniRx;

namespace Noneb.Core.InGameEditor.LevelEditing.Strongholds
{
    public class SetUpStrongholdInLevelCommand : ICommand
    {
        private readonly Coordinate _coordinate;
        private readonly ILevelDataEditingService _levelDataEditingService;
        private readonly ILevelDataRepository _levelDataRepository;
        private readonly IMapConfigRepository _mapConfigRepository;
        private readonly IMapEditingService _mapEditingService;
        private readonly IMapGetService _mapGetService;

        public SetUpStrongholdInLevelCommand(ILevelDataEditingService levelDataEditingService,
                                             IMapEditingService mapEditingService,
                                             IMapConfigRepository mapConfigRepository,
                                             IMapGetService mapGetService,
                                             ILevelDataRepository levelDataRepository,
                                             Coordinate coordinate)
        {
            _levelDataEditingService = levelDataEditingService;
            _mapEditingService = mapEditingService;
            _mapConfigRepository = mapConfigRepository;
            _mapGetService = mapGetService;
            _levelDataRepository = levelDataRepository;
            _coordinate = coordinate;
        }

        public IObservable<Unit> Do() =>
            GameActions.Stronghold.SetUpStrongholdInMap(_coordinate, _mapEditingService, _mapGetService)
                .SelectMany(
                    _ =>
                        EditorActions.Stronghold.SetUpStrongholdInLevelData(
                            _coordinate,
                            _levelDataEditingService,
                            _mapConfigRepository,
                            _levelDataRepository
                        )
                );

        public IObservable<Unit> Undo() => GameActions.Stronghold.DestructStrongholdInMap(_coordinate, _mapEditingService, _mapGetService)
            .SelectMany(
                _ =>
                    EditorActions.Stronghold.DestructStrongholdInLevelData(
                        _coordinate,
                        _levelDataEditingService,
                        _mapConfigRepository,
                        _levelDataRepository
                    )
            );

        public bool CanUndo => true;
    }
}