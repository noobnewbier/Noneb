using InGameEditor.Data;
using InGameEditor.Repositories.GameEnvironments;
using InGameEditor.Services.SaveEnvironment;
using UnityEngine;

namespace InGameEditor.Ui.Options.Save
{
    [CreateAssetMenu(menuName = "Factory/SaveViewModel", fileName = nameof(SaveViewModelFactory))]
    public class SaveViewModelFactory : ScriptableObject
    {
        [SerializeField] private SaveEnvironmentAsPreservationServiceProvider environmentAsPreservationServiceProvider;
        
        public SaveViewModel Create(EditorPalette editorPalette, ICurrentGameEnvironmentRepository repository)
        {
            return new SaveViewModel(environmentAsPreservationServiceProvider.Provide(), editorPalette, repository);
        }
    }
}