using System;
using Noneb.Core.Game.Common;
using Noneb.Core.Game.Common.Constants;
using Noneb.Core.Game.Common.Providers;
using UnityEngine;
using UnityEngine.UI;
using UnityUtils.Constants;

namespace Noneb.Ui.Game.UiState.CurrentGraphicRaycaster
{
    [CreateAssetMenu(fileName = nameof(CurrentGraphicRaycasterRepositoryProvider), menuName = MenuName.ScriptableRepository + ProjectMenuName.Current + "CurrentGraphicRaycasterRepository")]
    public class CurrentGraphicRaycasterRepositoryProvider : ScriptableObject, IObjectProvider<IDataRepository<GraphicRaycaster>>
    {
        private readonly Lazy<IDataRepository<GraphicRaycaster>> _lazyInstance = new Lazy<IDataRepository<GraphicRaycaster>>(() => new DataRepository<GraphicRaycaster>());
        public IDataRepository<GraphicRaycaster> Provide() => _lazyInstance.Value;
    }
}