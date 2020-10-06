using System;
using Main.Core.Game.Common.Providers;
using UnityEngine;
using UnityUtils.Constants;

namespace Main.Core.Game.InGameMessage
{
    [CreateAssetMenu(
        fileName = nameof(InGameMessageServiceProvider),
        menuName = MenuName.ScriptableService + nameof(InGameMessageService)
    )]
    public class InGameMessageServiceProvider : ScriptableObjectProvider<IInGameMessageService>
    {
        private readonly Lazy<IInGameMessageService> _lazyInstance =
            new Lazy<IInGameMessageService>(() => new InGameMessageService());

        public override IInGameMessageService Provide() => _lazyInstance.Value;
    }
}