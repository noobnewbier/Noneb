using GameEnvironments.Common.Repositories.CurrentGameEnvironments;
using GameEnvironments.Save.EditorOnly;
using InGameEditor.Services.InGameEditorMessage;
using UnityEngine;
using UnityUtils.Constants;

namespace InGameEditor.Ui.Options.Save
{
    [CreateAssetMenu(menuName = MenuName.Factory + nameof(SaveViewModel), fileName = nameof(SaveViewModelFactory))]
    public class SaveViewModelFactory : ScriptableObject
    {
        [SerializeField] private SaveEnvironmentAsScriptableServiceProvider saveEnvironmentAsScriptableServiceProvider;

        [SerializeField] private InGameEditorMessageServiceProvider messageServiceProvider;

        public SaveViewModel Create(ICurrentGameEnvironmentGetRepository getRepository) =>
            new SaveViewModel(
                saveEnvironmentAsScriptableServiceProvider.Provide(),
                getRepository,
                messageServiceProvider.Provide()
            );
    }
}