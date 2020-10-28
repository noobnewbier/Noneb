using Noneb.Core.Game.Common.Constants;
using Noneb.Core.Game.Common.Providers;
using Noneb.Ui.Game.UiState.CurrentGraphicRaycaster;
using Noneb.Ui.Game.UiState.CurrentUnityEventSystem;
using UnityEngine;
using UnityUtils.Constants;

namespace Noneb.Ui.Game.UiState.ClickStatus
{
    [CreateAssetMenu(fileName = nameof(ClickStatusServiceProvider), menuName = MenuName.ScriptableService + ProjectMenuName.Ui + nameof(ClickStatusService))]
    public class ClickStatusServiceProvider : ScriptableObject, IObjectProvider<IClickStatusService>
    {
        [SerializeField] private CurrentGraphicRaycasterRepositoryProvider graphicRaycasterRepositoryProvider;
        [SerializeField] private CurrentUnityEventSystemRepositoryProvider unityEventSystemRepositoryProvider;

        private IClickStatusService _cache;

        public IClickStatusService Provide()
        {
            return _cache ?? (_cache = new ClickStatusService(
                graphicRaycasterRepositoryProvider.Provide(),
                unityEventSystemRepositoryProvider.Provide()
            ));
        }
    }
}