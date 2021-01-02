using System;
using Noneb.Core.Game.Coordinates;
using Noneb.Core.Game.GameState.LevelDatas;
using Noneb.Core.Game.GameState.MapConfigs;
using Noneb.Core.Game.GameState.Maps;
using Noneb.Core.Game.Maps.MapModification;
using Noneb.Core.InGameEditor.LevelDataEditing;
using UniRx;

namespace Noneb.Core.InGameEditor.LevelEditing
{
    public interface ILevelEditingService
    {
        IObservable<Unit> ModifiedEventStream { get; }
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
        private readonly Subject<Unit> _modifiedEventStream;

        public LevelEditingService(ILevelDataEditingService levelDataEditingService,
                                IMapEditingService mapEditingService,
                                IMapConfigRepository mapConfigRepository,
                                IMapGetService mapGetService,
                                ILevelDataRepository levelDataRepository)
        {
            _modifiedEventStream = new Subject<Unit>();

            _levelDataEditingService = levelDataEditingService;
            _mapEditingService = mapEditingService;
            _mapConfigRepository = mapConfigRepository;
            _mapGetService = mapGetService;
            _levelDataRepository = levelDataRepository;
        }

        public IObservable<Unit> ModifiedEventStream => _modifiedEventStream;

        public IObservable<Unit> SetUpStronghold(Coordinate coordinate) =>
            SetUpStrongholdInMap(coordinate)
                .Concat(SetUpStrongholdInLevelData(coordinate))
                .Last()
                .DoOnCompleted(() => _modifiedEventStream.OnNext(Unit.Default));

        private IObservable<Unit> SetUpStrongholdInLevelData(Coordinate coordinate)
        {
            return _levelDataRepository.GetMostRecent()
                .Zip(_mapConfigRepository.GetMostRecent(), (data, config) => (data, config))
                .SelectMany(tuple => _levelDataEditingService.SetUpStronghold(tuple.data, tuple.config, coordinate));
        }

        private IObservable<Unit> SetUpStrongholdInMap(Coordinate coordinate)
        {
            return _mapGetService.GetMostRecent()
                .SelectMany(
                    m => _mapEditingService.SetUpStronghold(m, coordinate)
                );
        }
        

        public IObservable<Unit> DestructStronghold(Coordinate coordinate) =>
            DestructStrongholdInMap(coordinate)
                .Concat(DestructStrongholdInLevelData(coordinate))
                .Last()
                .DoOnCompleted(() => _modifiedEventStream.OnNext(Unit.Default));
        

        private IObservable<Unit> DestructStrongholdInLevelData(Coordinate coordinate)
        {
            return _levelDataRepository.GetMostRecent()
                .Zip(_mapConfigRepository.GetMostRecent(), (data, config) => (data, config))
                .SelectMany(tuple => _levelDataEditingService.DestructStronghold(tuple.data, tuple.config, coordinate));
        }

        private IObservable<Unit> DestructStrongholdInMap(Coordinate coordinate)
        {
            return _mapGetService.GetMostRecent()
                .SelectMany(
                    m => _mapEditingService.DestructStronghold(m, coordinate)
                );
        }
    }
}