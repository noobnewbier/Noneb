using System;
using Noneb.Core.Game.Common.Providers;
using UnityEngine;
using UnityUtils.Constants;

namespace Noneb.Core.InGameEditor.GetEnvironmentFilenameServices
{
    [CreateAssetMenu(
        fileName = nameof(GetEnvironmentFilenameServiceProvider),
        menuName = MenuName.ScriptableService + nameof(GetEnvironmentFilenameService)
    )]
    public class GetEnvironmentFilenameServiceProvider : ScriptableObject, IObjectProvider<IGetEnvironmentFilenameService>
    {
        private readonly Lazy<IGetEnvironmentFilenameService> _lazyInstance =
            new Lazy<IGetEnvironmentFilenameService>(() => new GetEnvironmentFilenameService());

        public IGetEnvironmentFilenameService Provide() => _lazyInstance.Value;
    }
}