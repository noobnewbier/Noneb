using System;
using Noneb.Core.Game.Common;
using Noneb.Core.Game.Common.Constants;
using Noneb.Core.Game.Common.Providers;
using Noneb.Core.InGameEditor.Common;
using UnityUtils.Constants;

namespace Noneb.Ui.InGameEditor.UiState.Inspectable
{
    [UnityEngine.CreateAssetMenu(fileName = nameof(CurrentInspectableRepositoryProvider), menuName = MenuName.ScriptableRepository + ProjectMenuName.InGameEditor + nameof(IInspectable))]
    public class CurrentInspectableRepositoryProvider : UnityEngine.ScriptableObject, IObjectProvider<IDataRepository<IInspectable>>
    {
        private readonly Lazy<IDataRepository<IInspectable>> _lazyInstance = new Lazy<IDataRepository<IInspectable>>(() => new DataRepository<IInspectable>());
        
        public IDataRepository<IInspectable> Provide() => _lazyInstance.Value;
    }
}