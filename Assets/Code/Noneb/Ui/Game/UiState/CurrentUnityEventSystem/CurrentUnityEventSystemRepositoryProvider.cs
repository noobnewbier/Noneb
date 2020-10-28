using System;
using Noneb.Core.Game.Common;
using Noneb.Core.Game.Common.Constants;
using Noneb.Core.Game.Common.Providers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityUtils.Constants;

namespace Noneb.Ui.Game.UiState.CurrentUnityEventSystem
{
    [CreateAssetMenu(fileName = nameof(CurrentUnityEventSystemRepositoryProvider), menuName = MenuName.ScriptableRepository + ProjectMenuName.Current + "CurrentUnityEventSystemRepository")]
    public class CurrentUnityEventSystemRepositoryProvider : ScriptableObject, IObjectProvider<IDataRepository<EventSystem>>
    {
        private readonly Lazy<IDataRepository<EventSystem>> _lazyInstance = new Lazy<IDataRepository<EventSystem>>(() => new DataRepository<EventSystem>());
        public IDataRepository<EventSystem> Provide() => _lazyInstance.Value;
    }
}