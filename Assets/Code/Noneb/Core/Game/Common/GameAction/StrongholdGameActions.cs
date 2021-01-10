using System;
using Noneb.Core.Game.Coordinates;
using Noneb.Core.Game.GameState.Maps;
using Noneb.Core.Game.Maps.MapModification;
using UniRx;

namespace Noneb.Core.Game.Common.GameAction
{
    public static partial class GameActions
    {
        public static class Stronghold
        {
            public static IObservable<Unit> SetUpStrongholdInMap(Coordinate coordinate,
                                                                 IMapEditingService mapEditingService,
                                                                 IMapGetService mapGetService)
            {
                return mapGetService.GetMostRecent()
                    .SelectMany(
                        m => mapEditingService.SetUpStronghold(m, coordinate)
                    );
            }

            public static IObservable<Unit> DestructStrongholdInMap(Coordinate coordinate,
                                                                    IMapEditingService mapEditingService,
                                                                    IMapGetService mapGetService)
            {
                return mapGetService.GetMostRecent()
                    .SelectMany(
                        m => mapEditingService.DestructStronghold(m, coordinate)
                    );
            }
        }
    }
}