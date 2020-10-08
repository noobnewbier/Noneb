using Main.Core.Game.Common.Providers;
using Main.Core.InGameEditor.GetEnvironmentFilenameServices;
using Main.Core.InGameEditor.GetInGameEditorDirectoryService;
using UnityEngine;
using UnityUtils.Constants;

namespace Main.Core.InGameEditor.Save.EditorOnly
{
    [CreateAssetMenu(
        fileName = nameof(SaveEnvironmentAsScriptableServiceProvider),
        menuName = MenuName.ScriptableService + nameof(SaveEnvironmentAsScriptableService)
    )]
    public class SaveEnvironmentAsScriptableServiceProvider : ScriptableObject, IObjectProvider<SaveEnvironmentAsScriptableService>
    {
        [SerializeField] private GetEnvironmentFilenameServiceProvider filenameServiceProvider;
        [SerializeField] private GetInGameEditorDirectoryServiceProvider getInGameEditorDirectoryServiceProvider;

        private SaveEnvironmentAsScriptableService _cache;

        public SaveEnvironmentAsScriptableService Provide() =>
            _cache ?? (_cache = new SaveEnvironmentAsScriptableService(
                filenameServiceProvider.Provide(),
                getInGameEditorDirectoryServiceProvider.Provide()
            ));
    }
}