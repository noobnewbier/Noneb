using System;
using GameEnvironments.Common.Repositories.CurrentGameEnvironment;
using GameEnvironments.Save;
using GameEnvironments.Save.EditorOnly;
using InGameEditor.Services;
using UnityEngine;

namespace InGameEditor.Ui.Options.Save
{
    [CreateAssetMenu(menuName = "Factory/SaveViewModel", fileName = nameof(SaveViewModelFactory))]
    public partial class SaveViewModelFactory : ScriptableObject
    {
        [SerializeField] private SaveEnvironmentAsPreservationServiceProvider saveEnvironmentAsPreservationServiceProvider;
        [SerializeField] private SaveEnvironmentAsScriptableServiceProvider saveEnvironmentAsScriptableServiceProvider;

        [SerializeField] private InGameEditorMessageServiceProvider messageServiceProvider;
        
        public SaveViewModel Create(ICurrentGameEnvironmentRepository repository ,SaveType saveType)
        {
            switch (saveType)
            {
                case SaveType.Scriptable:
                    return new SaveViewModel(
                        saveEnvironmentAsScriptableServiceProvider.Provide(),
                        repository,
                        messageServiceProvider.Provide()
                    );
                case SaveType.Preservation:
                    return new SaveViewModel(
                        saveEnvironmentAsPreservationServiceProvider.Provide(),
                        repository,
                        messageServiceProvider.Provide()
                    );
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}