using System;
using Common.Providers;
using UnityEngine;
using UnityUtils.Constants;

namespace Maps.Services
{
    [CreateAssetMenu(fileName = nameof(CoordinateServiceProvider), menuName = MenuName.ScriptableService + "GetCoordinateService")]
    public class CoordinateServiceProvider : ScriptableObjectProvider<ICoordinateService>
    {
        //prevent occasion where other classes are instantiated before this -- this should not have any dependencies anyway.
        private readonly Lazy<ICoordinateService> _lazyInstance = new Lazy<ICoordinateService>(() => new CoordinateService());

        public override ICoordinateService Provide() => _lazyInstance.Value;
    }
}