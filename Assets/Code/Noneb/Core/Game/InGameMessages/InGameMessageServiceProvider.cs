using System;
using Noneb.Core.Game.Common.Providers;
using UnityEngine;
using UnityUtils.Constants;

namespace Noneb.Core.Game.InGameMessages
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