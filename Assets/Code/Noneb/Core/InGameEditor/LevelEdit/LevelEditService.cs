using System;
using Noneb.Core.Game.Coordinates;
using Noneb.Core.Game.GameState.LevelDatas;
using Noneb.Core.Game.GameState.MapConfigs;
using Noneb.Core.Game.GameState.Maps;
using Noneb.Core.Game.Maps.MapModification;
using Noneb.Core.InGameEditor.LevelDataModification;
using UniRx;

namespace Noneb.Core.InGameEditor.LevelEdit
{
    public interface ILevelEditService
    {
        IObservable<Unit> ModifiedEventStream { get; }
        IObservable<Unit> SetUpStronghold(Coordinate coordinate);
        IObservable<Unit> DestructStronghold(Coordinate coordinate);
    }

    public class LevelEditService : ILevelEditService
    {
        private readonly ILevelDataModificationService _levelDataModificationService;
        private readonly IMapModificationService _mapModificationService;
        private readonly IMapConfigRepository _mapConfigRepository;
        private readonly IMapGetService _mapGetService;
        private readonly ILevelDataRepository _levelDataRepository;

        public LevelEditService(ILevelDataModificationService levelDataModificationService,
                                IMapModificationService mapModificationService,
                                IMapConfigRepository mapConfigRepository,
                                IMapGetService mapGetService,
                                ILevelDataRepository levelDataRepository)
        {
            ModifiedEventStream = new Subject<Unit>();

            _levelDataModificationService = levelDataModificationService;
            _mapModificationService = mapModificationService;
            _mapConfigRepository = mapConfigRepository;
            _mapGetService = mapGetService;
            _levelDataRepository = levelDataRepository;
        }

        public IObservable<Unit> ModifiedEventStream { get; }

        public IObservable<Unit> SetUpStronghold(Coordinate coordinate) =>
            SetUpStrongholdInMap(coordinate)
                .Concat(SetUpStrongholdInLevelData(coordinate))
                .Last();


        public IObservable<Unit> DestructStronghold(Coordinate coordinate) =>
            DestructStrongholdInMap(coordinate)
                .Concat(DestructStrongholdInLevelData(coordinate))
                .Last();

        private IObservable<Unit> SetUpStrongholdInLevelData(Coordinate coordinate)
        {
            return _levelDataRepository.GetMostRecent()
                .Zip(_mapConfigRepository.GetMostRecent(), (data, config) => (data, config))
                .SelectMany(tuple => _levelDataModificationService.SetUpStronghold(tuple.data, tuple.config, coordinate));
        }

        private IObservable<Unit> SetUpStrongholdInMap(Coordinate coordinate)
        {
            return _mapGetService.GetMostRecent()
                .SelectMany(
                    m => _mapModificationService.SetUpStronghold(m, coordinate)
                );
        }

        private IObservable<Unit> DestructStrongholdInLevelData(Coordinate coordinate)
        {
            return _levelDataRepository.GetMostRecent()
                .Zip(_mapConfigRepository.GetMostRecent(), (data, config) => (data, config))
                .SelectMany(tuple => _levelDataModificationService.DestructStronghold(tuple.data, tuple.config, coordinate));
        }

        private IObservable<Unit> DestructStrongholdInMap(Coordinate coordinate)
        {
            return _mapGetService.GetMostRecent()
                .SelectMany(
                    m => _mapModificationService.DestructStronghold(m, coordinate)
                );
        }
    }
}