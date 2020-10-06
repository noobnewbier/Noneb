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
    public class InGameEditorCameraRepositoryProvider : ScriptableObjectProvider<InGameEditorCameraRepository>
    {
        private readonly Lazy<InGameEditorCameraRepository> _lazyInstance =
            new Lazy<InGameEditorCameraRepository>(() => new InGameEditorCameraRepository());

        public override InGameEditorCameraRepository Provide() => _lazyInstance.Value;
    }
}