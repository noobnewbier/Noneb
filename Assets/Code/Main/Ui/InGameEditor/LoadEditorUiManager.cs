using Main.Ui.InGameEditor.Cameras;
using UnityEngine;

namespace Main.Ui.InGameEditor
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