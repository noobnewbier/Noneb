using InGameEditor.Cameras;
using UnityEngine;

namespace InGameEditor
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