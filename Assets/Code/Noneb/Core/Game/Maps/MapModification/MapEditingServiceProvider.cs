using System;
using Noneb.Core.Game.Common.Constants;
using Noneb.Core.Game.Common.Providers;
using UnityEngine;
using UnityUtils.Constants;

namespace Noneb.Core.Game.Maps.MapModification
{
    [CreateAssetMenu(fileName = nameof(MapEditingServiceProvider), menuName = MenuName.ScriptableService + ProjectMenuName.InGameEditor + nameof(MapEditingService))]
    public class MapEditingServiceProvider : ScriptableObject, IObjectProvider<IMapEditingService>
    {
        private readonly Lazy<IMapEditingService> _lazyInstance = new Lazy<IMapEditingService>(() => new MapEditingService());
        
        public IMapEditingService Provide() => _lazyInstance.Value;
    }
}