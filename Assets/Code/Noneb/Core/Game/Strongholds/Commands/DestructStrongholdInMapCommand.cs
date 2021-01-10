using System;
using Noneb.Core.Game.Commands;
using Noneb.Core.Game.Common.GameAction;
using Noneb.Core.Game.Coordinates;
using Noneb.Core.Game.GameState.Maps;
using Noneb.Core.Game.Maps.MapModification;
using UniRx;

namespace Noneb.Core.Game.Strongholds.Commands
{
    public class DestructStrongholdInMapCommand : ICommand
    {
        private readonly Coordinate _coordinate;
        private readonly IMapEditingService _mapEditingService;

        private readonly IMapGetService _mapGetService;

        public DestructStrongholdInMapCommand(IMapEditingService mapEditingService,
                                              IMapGetService mapGetService,
                                              Coordinate coordinate)
        {
            _mapEditingService = mapEditingService;
            _mapGetService = mapGetService;
            _coordinate = coordinate;
        }

        public IObservable<Unit> Do() => GameActions.Stronghold.DestructStrongholdInMap(_coordinate, _mapEditingService, _mapGetService);
        
        public IObservable<Unit> Undo() => GameActions.Stronghold.SetUpStrongholdInMap(_coordinate, _mapEditingService, _mapGetService);
        public bool CanUndo => true;
    }
}