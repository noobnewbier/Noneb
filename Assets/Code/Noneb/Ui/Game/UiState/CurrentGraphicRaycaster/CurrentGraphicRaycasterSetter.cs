using UnityEngine;
using UnityEngine.UI;

namespace Noneb.Ui.Game.UiState.CurrentGraphicRaycaster
{
    public class CurrentGraphicRaycasterSetter : MonoBehaviour
    {
        [SerializeField] private CurrentGraphicRaycasterRepositoryProvider repositoryProvider;
        [SerializeField] private GraphicRaycaster graphicRaycaster;
        
        public void Set()
        {
            repositoryProvider.Provide().Set(graphicRaycaster);
        }
    }
}