using UnityEngine;

namespace Main.Ui.InGameEditor.Cameras
{
    public class InGameEditorCameraRepositorySetter : MonoBehaviour
    {
        [SerializeField] private InGameEditorCameraRepositoryProvider provider;
        [SerializeField] private Camera editorCamera;

        [ContextMenu(nameof(Set))]
        public void Set()
        {
            provider.Provide().Set(editorCamera);
        }
    }
}