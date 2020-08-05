using System;
using Common.Providers;
using UnityEngine;
using UnityUtils.Constants;

namespace InGameEditor.Services
{
    [CreateAssetMenu(
        fileName = nameof(InGameEditorMessageServiceProvider),
        menuName = MenuName.ScriptableService + nameof(InGameEditorMessageService)
    )]
    public class InGameEditorMessageServiceProvider : ScriptableObjectProvider<IInGameEditorMessageService>
    {
        private readonly Lazy<IInGameEditorMessageService> _lazyInstance =
            new Lazy<IInGameEditorMessageService>(() => new InGameEditorMessageService());

        public override IInGameEditorMessageService Provide()
        {
            return _lazyInstance.Value;
        }
    }
}