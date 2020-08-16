using System;
using GameEnvironments.Common.Repositories.CurrentGameEnvironment;
using GameEnvironments.Save.EditorOnly;
using InGameEditor.Services.InGameEditorMessageServices;
using ObsoleteJsonRelated;
using UnityEngine;
using UnityEngine.Serialization;
using UnityUtils.Constants;

namespace InGameEditor.Ui.Options.Save
{
    [CreateAssetMenu(menuName = MenuName.Factory + nameof(SaveViewModel), fileName = nameof(SaveViewModelFactory))]
    public class SaveViewModelFactory : ScriptableObject
    {
        [FormerlySerializedAs("saveEnvironmentAsPreservationServiceProvider")] [SerializeField]
        private SaveEnvironmentAsJsonServiceProvider saveEnvironmentAsJsonServiceProvider;

        [SerializeField] private SaveEnvironmentAsScriptableServiceProvider saveEnvironmentAsScriptableServiceProvider;

        [SerializeField] private InGameEditorMessageServiceProvider messageServiceProvider;

        public SaveViewModel Create(ICurrentGameEnvironmentGetRepository getRepository, SaveType saveType)
        {
            switch (saveType)
            {
                case SaveType.Scriptable:
                    return new SaveViewModel(
                        saveEnvironmentAsScriptableServiceProvider.Provide(),
                        getRepository,
                        messageServiceProvider.Provide()
                    );
                case SaveType.Preservation:
                    return new SaveViewModel(
                        saveEnvironmentAsJsonServiceProvider.Provide(),
                        getRepository,
                        messageServiceProvider.Provide()
                    );
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}