using Noneb.Ui.InGameEditor.Cameras;
using UnityEngine;

namespace Noneb.Ui.InGameEditor
{
    public class LoadEditorUiManager : MonoBehaviour
    {
        [SerializeField] private InGameEditorCameraRepositorySetter cameraRepositorySetter;

        private void OnEnable()
        {
            cameraRepositorySetter.Set();
        }
    }
}