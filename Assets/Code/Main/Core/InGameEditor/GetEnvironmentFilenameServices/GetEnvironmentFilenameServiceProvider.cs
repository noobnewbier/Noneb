using System;
using Main.Core.Game.Common.Providers;
using UnityEngine;
using UnityUtils.Constants;

namespace Main.Core.InGameEditor.GetEnvironmentFilenameServices
{
    [CreateAssetMenu(
        fileName = nameof(GetEnvironmentFilenameServiceProvider),
        menuName = MenuName.ScriptableService + nameof(GetEnvironmentFilenameService)
    )]
    public class GetEnvironmentFilenameServiceProvider : ScriptableObjectProvider<IGetEnvironmentFilenameService>
    {
        private readonly Lazy<IGetEnvironmentFilenameService> _lazyInstance =
            new Lazy<IGetEnvironmentFilenameService>(() => new GetEnvironmentFilenameService());

        public override IGetEnvironmentFilenameService Provide() => _lazyInstance.Value;
    }
}