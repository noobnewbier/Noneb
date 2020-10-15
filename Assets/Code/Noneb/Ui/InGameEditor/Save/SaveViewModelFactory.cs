using Noneb.Core.Game.GameState.CurrentGameEnvironments;
using Noneb.Core.Game.InGameMessages;
using Noneb.Core.InGameEditor.Save.EditorOnly;
using UnityEngine;
using UnityUtils.Constants;

namespace Noneb.Ui.InGameEditor.Save
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