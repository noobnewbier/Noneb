using System;
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
                    throw new InvalidOperationException($"{nameof(SaveType.Preservation)} is now obsolete");
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}