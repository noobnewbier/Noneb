using Noneb.Ui.InGameEditor.Cameras;
using UnityEngine;

namespace Noneb.Ui.InGameEditor
{
    public class LoadEditorUiManager : MonoBehaviour
    {
        [SerializeField] private CameraRepositorySetter cameraRepositorySetter;

        private void OnEnable()
        {
            cameraRepositorySetter.Set();
        }
    }
}