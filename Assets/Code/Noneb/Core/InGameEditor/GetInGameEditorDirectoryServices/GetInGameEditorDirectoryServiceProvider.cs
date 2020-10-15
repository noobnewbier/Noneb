using System;
using Noneb.Core.Game.Common.Providers;
using UnityEngine;
using UnityUtils.Constants;

namespace Noneb.Core.InGameEditor.GetInGameEditorDirectoryServices
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