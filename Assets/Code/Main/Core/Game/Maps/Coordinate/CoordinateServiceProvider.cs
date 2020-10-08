using System;
using Main.Core.Game.Common.Providers;
using UnityEngine;
using UnityUtils.Constants;

namespace Main.Core.Game.Maps.Coordinate
{
    [CreateAssetMenu(fileName = nameof(CoordinateServiceProvider), menuName = MenuName.ScriptableService + "GetCoordinateService")]
    public class CoordinateServiceProvider : ScriptableObject, IObjectProvider<ICoordinateService>
    {
        //prevent occasion where other classes are instantiated before this -- this should not have any dependencies anyway.
        private readonly Lazy<ICoordinateService> _lazyInstance = new Lazy<ICoordinateService>(() => new CoordinateService());

        public ICoordinateService Provide() => _lazyInstance.Value;
    }
}