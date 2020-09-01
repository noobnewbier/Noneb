using System;
using Common.Providers;
using UnityEngine;
using UnityUtils.Constants;

namespace Maps.Services
{
    [CreateAssetMenu(fileName = nameof(GetCoordinateServiceProvider), menuName = MenuName.ScriptableService + "GetCoordinateService")]
    public class GetCoordinateServiceProvider : ScriptableObjectProvider<IGetCoordinateService>
    {
        //prevent occasion where other classes are instantiated before this -- this should not have any dependencies anyway.
        private readonly Lazy<IGetCoordinateService> _lazyInstance = new Lazy<IGetCoordinateService>(() => new GetCoordinateService());

        public override IGetCoordinateService Provide()
        {
            return _lazyInstance.Value;
        }
    }
}