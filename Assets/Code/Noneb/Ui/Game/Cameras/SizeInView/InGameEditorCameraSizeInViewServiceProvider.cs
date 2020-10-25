using System;
using Noneb.Core.Game.Common.Providers;
using UnityEngine;
using UnityUtils.Constants;

namespace Noneb.Ui.Game.Cameras.SizeInView
{
    [CreateAssetMenu(
        fileName = nameof(InGameEditorCameraSizeInViewServiceProvider),
        menuName = MenuName.ScriptableService + nameof(InGameEditorCameraSizeInViewService)
    )]
    public class InGameEditorCameraSizeInViewServiceProvider : ScriptableObject, IObjectProvider<IInGameEditorCameraSizeInViewService>
    {
        private readonly Lazy<IInGameEditorCameraSizeInViewService> _lazyInstance =
            new Lazy<IInGameEditorCameraSizeInViewService>(() => new InGameEditorCameraSizeInViewService());

        public IInGameEditorCameraSizeInViewService Provide() => _lazyInstance.Value;
    }
}