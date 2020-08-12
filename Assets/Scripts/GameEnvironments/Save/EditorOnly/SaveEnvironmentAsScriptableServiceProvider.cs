using Common.Providers;
using GameEnvironments.Common.Services.GetEnvironmentFilenameServices;
using GameEnvironments.Common.Services.GetInGameEditorDirectoryService;
using UnityEngine;
using UnityUtils.Constants;

namespace GameEnvironments.Save.EditorOnly
{
    [CreateAssetMenu(
        fileName = nameof(SaveEnvironmentAsScriptableServiceProvider),
        menuName = MenuName.ScriptableService + nameof(SaveEnvironmentAsScriptableService)
    )]
    public class SaveEnvironmentAsScriptableServiceProvider : ScriptableObjectProvider<SaveEnvironmentAsScriptableService>
    {
        [SerializeField] private GetEnvironmentFilenameServiceProvider filenameServiceProvider;
        [SerializeField] private GetInGameEditorDirectoryServiceProvider getInGameEditorDirectoryServiceProvider;


        private SaveEnvironmentAsScriptableService _cache;

        public override SaveEnvironmentAsScriptableService Provide()
        {
            return _cache ?? (_cache = new SaveEnvironmentAsScriptableService(
                filenameServiceProvider.Provide(),
                getInGameEditorDirectoryServiceProvider.Provide()
            ));
        }
    }
}