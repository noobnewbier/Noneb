using EventManagement.Providers;
using UnityEngine;

namespace InGameEditor.Ui.EditorMessageShower
{
    [CreateAssetMenu(fileName = nameof(EditorMessageViewModelFactory), menuName = "Factory/EditorMessageViewModel")]
    public class EditorMessageViewModelFactory : ScriptableObject
    {
        [SerializeField] private EventAggregatorProvider uiEventAggregatorProvider;
        
        public IEditorMessageViewModel Create()
        {
            return new EditorMessageViewModel(uiEventAggregatorProvider.ProvideEventAggregator());
        }
    }
}