using System;
using Main.Core.Game.Common.Providers;
using UnityEngine;
using UnityUtils.Constants;

namespace Main.Ui.InGameEditor.Cameras.SizeInView
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