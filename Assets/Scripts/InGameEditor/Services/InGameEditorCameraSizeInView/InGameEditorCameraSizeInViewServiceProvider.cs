using System;
using Common.Providers;
using UnityEngine;
using UnityUtils.Constants;

namespace InGameEditor.Services.InGameEditorCameraSizeInView
{
    [CreateAssetMenu(
        fileName = nameof(InGameEditorCameraSizeInViewServiceProvider),
        menuName = MenuName.ScriptableService + nameof(InGameEditorCameraSizeInViewService)
    )]
    public class InGameEditorCameraSizeInViewServiceProvider : ScriptableObjectProvider<IInGameEditorCameraSizeInViewService>
    {
        private readonly Lazy<IInGameEditorCameraSizeInViewService> _lazyInstance =
            new Lazy<IInGameEditorCameraSizeInViewService>(() => new InGameEditorCameraSizeInViewService());

        public override IInGameEditorCameraSizeInViewService Provide() => _lazyInstance.Value;
    }
}