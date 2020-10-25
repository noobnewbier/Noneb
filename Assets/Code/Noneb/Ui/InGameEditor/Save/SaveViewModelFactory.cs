using Noneb.Core.Game.Common.Factories;
using Noneb.Core.Game.GameState.GameEnvironments;
using Noneb.Core.Game.InGameMessages;
using Noneb.Core.InGameEditor.Save.EditorOnly;
using UnityEngine;
using UnityUtils.Constants;

namespace Noneb.Ui.InGameEditor.Save
{
    [CreateAssetMenu(menuName = MenuName.Factory + nameof(SaveViewModel), fileName = nameof(SaveViewModelFactory))]
    public class SaveViewModelFactory : ScriptableObject, IFactory<SaveViewModel>
    {
        [SerializeField] private SaveEnvironmentAsScriptableServiceProvider saveEnvironmentAsScriptableServiceProvider;
        [SerializeField] private GameEnvironmentRepositoryProvider loadedGameEnvironmentRepositoryProvider;
        [SerializeField] private InGameMessageServiceProvider messageServiceProvider;

        public SaveViewModel Create() =>
            new SaveViewModel(
                saveEnvironmentAsScriptableServiceProvider.Provide(),
                loadedGameEnvironmentRepositoryProvider.Provide(),
                messageServiceProvider.Provide()
            );
    }
}