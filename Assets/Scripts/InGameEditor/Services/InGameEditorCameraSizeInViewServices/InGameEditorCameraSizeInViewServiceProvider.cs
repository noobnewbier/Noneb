using System;
using Common.Providers;
using UnityEngine;
using UnityUtils.Constants;

namespace InGameEditor.Services.InGameEditorCameraSizeInViewServices
{
    [CreateAssetMenu(fileName = nameof(InGameEditorCameraSizeInViewServiceProvider), menuName = MenuName.ScriptableService + nameof(InGameEditorCameraSizeInViewService))]
    public class InGameEditorCameraSizeInViewServiceProvider : ScriptableObjectProvider<IInGameEditorCameraSizeInViewService>
    {
        private readonly Lazy<IInGameEditorCameraSizeInViewService> _lazyInstance = new Lazy<IInGameEditorCameraSizeInViewService>(() => new InGameEditorCameraSizeInViewService());
        public override IInGameEditorCameraSizeInViewService Provide()
        {
            return _lazyInstance.Value;
        }
    }
}