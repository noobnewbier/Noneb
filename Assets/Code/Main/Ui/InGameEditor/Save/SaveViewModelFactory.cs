using Main.Core.Game.GameState.CurrentGameEnvironments;
using Main.Core.Game.InGameMessages;
using Main.Core.InGameEditor.Save.EditorOnly;
using UnityEngine;
using UnityUtils.Constants;

namespace Main.Ui.InGameEditor.Save
{
    [CreateAssetMenu(menuName = MenuName.Factory + nameof(SaveViewModel), fileName = nameof(SaveViewModelFactory))]
    public class SaveViewModelFactory : ScriptableObject
    {
        [SerializeField] private SaveEnvironmentAsScriptableServiceProvider saveEnvironmentAsScriptableServiceProvider;

        [SerializeField] private InGameMessageServiceProvider messageServiceProvider;

        public SaveViewModel Create(ICurrentGameEnvironmentGetRepository getRepository) =>
            new SaveViewModel(
                saveEnvironmentAsScriptableServiceProvider.Provide(),
                getRepository,
                messageServiceProvider.Provide()
            );
    }
}