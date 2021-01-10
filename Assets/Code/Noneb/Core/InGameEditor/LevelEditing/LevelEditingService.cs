using System;
using Noneb.Core.Game.Commands;
using Noneb.Core.Game.Coordinates;
using Noneb.Core.Game.GameState.LevelDatas;
using Noneb.Core.Game.GameState.MapConfigs;
using Noneb.Core.Game.GameState.Maps;
using Noneb.Core.Game.Maps.MapModification;
using Noneb.Core.InGameEditor.LevelDataEditing;
using Noneb.Core.InGameEditor.LevelEditing.Strongholds;
using UniRx;

namespace Noneb.Core.InGameEditor.LevelEditing
{
    public interface ILevelEditingService
    {
        IObservable<Unit> SetUpStronghold(Coordinate coordinate);
        IObservable<Unit> DestructStronghold(Coordinate coordinate);
    }

    public class LevelEditingService : ILevelEditingService
    {
        private readonly ILevelDataEditingService _levelDataEditingService;
        private readonly ILevelDataRepository _levelDataRepository;
        private readonly IMapConfigRepository _mapConfigRepository;
        private readonly IMapGetService _mapGetService;
        private readonly IMapEditingService _mapEditingService;
        private readonly ICommandExecutionService _commandExecutionService;

        public LevelEditingService(ILevelDataEditingService levelDataEditingService,
                                   IMapEditingService mapEditingService,
                                   IMapConfigRepository mapConfigRepository,
                                   IMapGetService mapGetService,
                                   ILevelDataRepository levelDataRepository,
                                   ICommandExecutionService commandExecutionService)
        {
            _levelDataEditingService = levelDataEditingService;
            _mapEditingService = mapEditingService;
            _mapConfigRepository = mapConfigRepository;
            _mapGetService = mapGetService;
            _levelDataRepository = levelDataRepository;
            _commandExecutionService = commandExecutionService;
        }

        public IObservable<Unit> SetUpStronghold(Coordinate coordinate) =>
            _commandExecutionService.Do(
                new SetUpStrongholdInLevelCommand(
                    _levelDataEditingService,
                    _mapEditingService,
                    _mapConfigRepository,
                    _mapGetService,
                    _levelDataRepository,
                    coordinate
                )
            );

        public IObservable<Unit> DestructStronghold(Coordinate coordinate) =>
            _commandExecutionService.Do(
                new DestructStrongholdInLevelCommand(
                    _levelDataEditingService,
                    _mapEditingService,
                    _mapConfigRepository,
                    _mapGetService,
                    _levelDataRepository,
                    coordinate
                )
            );
    }
}