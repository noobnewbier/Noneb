using System;
using Main.Core.Game.Common.Providers;
using UnityEngine;
using UnityUtils.Constants;

namespace Main.Ui.InGameEditor.Cameras
{
    [CreateAssetMenu(
        fileName = nameof(InGameEditorCameraRepositoryProvider),
        menuName = MenuName.ScriptableRepository + nameof(InGameEditorCameraRepository)
    )]
    public class InGameEditorCameraRepositoryProvider : ScriptableObject, IObjectProvider<InGameEditorCameraRepository>
    {
        private readonly Lazy<InGameEditorCameraRepository> _lazyInstance =
            new Lazy<InGameEditorCameraRepository>(() => new InGameEditorCameraRepository());

        public InGameEditorCameraRepository Provide() => _lazyInstance.Value;
    }
}