using InGameEditor.Services.InGameEditorMessage;
using UnityEngine;
using UnityUtils.Constants;

namespace InGameEditor.Ui.EditorMessageShower
{
    [CreateAssetMenu(fileName = nameof(DebugLogMessageViewModelFactory), menuName = MenuName.Factory + nameof(DebugLogMessageViewModel))]
    public class DebugLogMessageViewModelFactory : ScriptableObject
    {
        [SerializeField] private InGameMessageServiceProvider messageServiceProvider;

        public IDebugLogMessageViewModel Create() => new DebugLogMessageViewModel(messageServiceProvider.Provide());
    }
}