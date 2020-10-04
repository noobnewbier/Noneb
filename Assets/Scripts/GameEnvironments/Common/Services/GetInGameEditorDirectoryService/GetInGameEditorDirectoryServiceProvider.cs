using System;
using Common.Providers;
using UnityEngine;
using UnityUtils.Constants;

namespace GameEnvironments.Common.Services.GetInGameEditorDirectoryService
{
    [CreateAssetMenu(
        fileName = nameof(GetInGameEditorDirectoryServiceProvider),
        menuName = MenuName.ScriptableService + nameof(GetInGameEditorDirectoryService)
    )]
    public class GetInGameEditorDirectoryServiceProvider : ScriptableObjectProvider<IGetInGameEditorDirectoryService>
    {
        private readonly Lazy<IGetInGameEditorDirectoryService> _lazyInstance =
            new Lazy<IGetInGameEditorDirectoryService>(() => new GetInGameEditorDirectoryService());

        public override IGetInGameEditorDirectoryService Provide() => _lazyInstance.Value;
    }
}