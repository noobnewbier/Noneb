using InGameEditor.Services.InGameEditorMessage;
using UnityEngine;
using UnityUtils.Constants;

namespace InGameEditor.Ui.EditorMessageShower
{
    [CreateAssetMenu(fileName = nameof(EditorMessageViewModelFactory), menuName = MenuName.Factory + nameof(EditorMessageViewModel))]
    public class EditorMessageViewModelFactory : ScriptableObject
    {
        [SerializeField] private InGameEditorMessageServiceProvider messageServiceProvider;

        public IEditorMessageViewModel Create() => new EditorMessageViewModel(messageServiceProvider.Provide());
    }
}