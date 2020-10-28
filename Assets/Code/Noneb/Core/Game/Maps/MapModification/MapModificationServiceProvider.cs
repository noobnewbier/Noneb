using System;
using Noneb.Core.Game.Common.Constants;
using Noneb.Core.Game.Common.Providers;
using UnityEngine;
using UnityUtils.Constants;

namespace Noneb.Core.Game.Maps.MapModification
{
    [CreateAssetMenu(fileName = nameof(MapModificationServiceProvider), menuName = MenuName.ScriptableService + ProjectMenuName.InGameEditor + nameof(MapModificationService))]
    public class MapModificationServiceProvider : ScriptableObject, IObjectProvider<IMapModificationService>
    {
        private readonly Lazy<IMapModificationService> _lazyInstance = new Lazy<IMapModificationService>(() => new MapModificationService());
        
        public IMapModificationService Provide() => _lazyInstance.Value;
    }
}