using Experiment.NoobAutoLinker.Core;
using Noneb.Core.Game.Common.Constants;
using Noneb.Core.Game.Common.Factories;
using Noneb.Core.Game.GameState.Maps;
using Noneb.Core.Game.Maps.MapModification;
using Noneb.Ui.InGameEditor.UiState.Inspectable;
using UnityEngine;
using UnityUtils.Constants;

namespace Noneb.Ui.InGameEditor.Inspector.UnitInspector
{
    [CreateAssetMenu(
        fileName = nameof(UnitInspectorViewModelFactory),
        menuName = MenuName.Factory + ProjectMenuName.InGameEditor + "UnitInspectorViewModelFactory"
    )]
    public class UnitInspectorViewModelFactory : ScriptableObject, IFactory<UnitInspectorViewModel>
    {
        [SerializeField] private CurrentInspectableRepositoryProvider currentInspectableRepositoryProvider;
        [AutoLink] [SerializeField] private MapEditingServiceProvider mapEditingServiceProvider;
        [SerializeField] private MapRepositoryProvider mapRepositoryProvider;


        public UnitInspectorViewModel Create() =>
            new UnitInspectorViewModel(
                currentInspectableRepositoryProvider.Provide(),
                mapRepositoryProvider.Provide(),
                mapEditingServiceProvider.Provide()
            );
    }
}