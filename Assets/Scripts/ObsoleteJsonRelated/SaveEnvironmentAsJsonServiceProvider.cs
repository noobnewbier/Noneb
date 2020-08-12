using Common.Providers;
using GameEnvironments.Common.Services.GetEnvironmentFilenameServices;
using GameEnvironments.Common.Services.GetInGameEditorDirectoryService;
using GameEnvironments.Save;
using InGameEditor.Repositories.SelectedEditorPalettes;
using UnityEngine;
using UnityUtils.Constants;

namespace ObsoleteJsonRelated
{
    [CreateAssetMenu(
        menuName = MenuName.ScriptableService + nameof(SaveEnvironmentAsJsonService),
        fileName = nameof(SaveEnvironmentAsJsonServiceProvider)
    )]
    public class SaveEnvironmentAsJsonServiceProvider : ScriptableObjectProvider<ISaveEnvironmentService>
    {
        [SerializeField] private SelectedEditorPaletteRepositoryProvider editorPaletteRepositoryProvider;
        [SerializeField] private GetEnvironmentFilenameServiceProvider filenameServiceProvider;
        [SerializeField] private GetInGameEditorDirectoryServiceProvider getInGameEditorDirectoryServiceProvider;

        private ISaveEnvironmentService _cache;

        public override ISaveEnvironmentService Provide()
        {
            return _cache ?? (_cache = new SaveEnvironmentAsJsonService(
                editorPaletteRepositoryProvider.Provide(),
                filenameServiceProvider.Provide(),
                getInGameEditorDirectoryServiceProvider.Provide()
            ));
        }
    }
}