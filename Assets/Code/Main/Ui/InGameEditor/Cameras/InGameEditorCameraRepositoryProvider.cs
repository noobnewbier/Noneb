using System;
using Common.Providers;
using UnityEngine;
using UnityUtils.Constants;

namespace InGameEditor.Repositories.InGameEditorCamera
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