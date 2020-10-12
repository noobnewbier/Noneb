using System;
using Main.Core.Game.Common.Providers;
using UnityEngine;
using UnityUtils.Constants;

namespace Main.Core.Game.InGameMessages
{
    [CreateAssetMenu(
        fileName = nameof(InGameMessageServiceProvider),
        menuName = MenuName.ScriptableService + nameof(InGameMessageService)
    )]
    public class InGameMessageServiceProvider : ScriptableObject, IObjectProvider<IInGameMessageService>
    {
        private readonly Lazy<IInGameMessageService> _lazyInstance =
            new Lazy<IInGameMessageService>(() => new InGameMessageService());

        public IInGameMessageService Provide() => _lazyInstance.Value;
    }
}