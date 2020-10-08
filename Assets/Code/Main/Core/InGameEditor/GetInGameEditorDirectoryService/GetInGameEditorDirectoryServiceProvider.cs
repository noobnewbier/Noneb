using System;
using Main.Core.Game.Common.Providers;
using UnityEngine;
using UnityUtils.Constants;

namespace Main.Core.InGameEditor.GetInGameEditorDirectoryService
{
    [CreateAssetMenu(
        fileName = nameof(GetInGameEditorDirectoryServiceProvider),
        menuName = MenuName.ScriptableService + nameof(GetInGameEditorDirectoryService)
    )]
    public class GetInGameEditorDirectoryServiceProvider : ScriptableObject, IObjectProvider<IGetInGameEditorDirectoryService>
    {
        private readonly Lazy<IGetInGameEditorDirectoryService> _lazyInstance =
            new Lazy<IGetInGameEditorDirectoryService>(() => new GetInGameEditorDirectoryService());

        public IGetInGameEditorDirectoryService Provide() => _lazyInstance.Value;
    }
}