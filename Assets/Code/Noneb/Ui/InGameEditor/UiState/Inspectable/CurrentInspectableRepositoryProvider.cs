using System;
using Noneb.Core.Game.Common;
using Noneb.Core.Game.Common.Constants;
using Noneb.Core.Game.Common.Providers;
using Noneb.Core.Game.Common.TagInterface;
using UnityEngine;
using UnityUtils.Constants;

namespace Noneb.Ui.InGameEditor.UiState.Inspectable
{
    [CreateAssetMenu(
        fileName = nameof(CurrentInspectableRepositoryProvider),
        menuName = MenuName.ScriptableRepository + ProjectMenuName.InGameEditor + ProjectMenuName.Current + nameof(IInspectable)
    )]
    public class CurrentInspectableRepositoryProvider : ScriptableObject, IObjectProvider<IDataRepository<IInspectable>>
    {
        private readonly Lazy<IDataRepository<IInspectable>> _lazyInstance =
            new Lazy<IDataRepository<IInspectable>>(() => new DataRepository<IInspectable>());

        public IDataRepository<IInspectable> Provide() => _lazyInstance.Value;
    }
}