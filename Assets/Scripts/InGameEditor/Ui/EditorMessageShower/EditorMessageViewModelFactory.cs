using EventManagement.Providers;
using InGameEditor.Services;
using UnityEngine;

namespace InGameEditor.Ui.EditorMessageShower
{
    [CreateAssetMenu(fileName = nameof(EditorMessageViewModelFactory), menuName = "Factory/EditorMessageViewModel")]
    public class EditorMessageViewModelFactory : ScriptableObject
    {
        [SerializeField] private InGameEditorMessageServiceProvider messageServiceProvider;
                
        public IEditorMessageViewModel Create()
        {
            return new EditorMessageViewModel(messageServiceProvider.Provide());
        }
    }
}