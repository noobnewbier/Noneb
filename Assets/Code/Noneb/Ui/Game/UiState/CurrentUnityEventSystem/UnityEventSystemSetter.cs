using UnityEngine;
using UnityEngine.EventSystems;

namespace Noneb.Ui.Game.UiState.CurrentUnityEventSystem
{
    public class UnityEventSystemSetter : MonoBehaviour
    {
        [SerializeField] private EventSystem eventSystem;
        [SerializeField] private CurrentUnityEventSystemRepositoryProvider repositoryProvider;

        public void Set()
        {
            repositoryProvider.Provide().Set(eventSystem);
        }
    }
}