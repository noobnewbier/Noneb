using Experiment.NoobAutoLinker.Core;
using Noneb.Core.Game.Common.Constants;
using Noneb.Core.Game.Common.Factories;
using Noneb.Core.Game.GameState.Maps;
using Noneb.Core.Game.Maps.MapModification;
using Noneb.Ui.InGameEditor.UiState.Inspectable;
using UnityEngine;
using UnityUtils.Constants;

namespace Noneb.Ui.InGameEditor.Inspector.ConstructInspector
{
    [CreateAssetMenu(
        fileName = nameof(ConstructInspectorViewModelFactory),
        menuName = MenuName.Factory + ProjectMenuName.InGameEditor + "ConstructInspectorViewModel"
    )]
    public class ConstructInspectorViewModelFactory : ScriptableObject, IFactory<ConstructInspectorViewModel>
    {
        [SerializeField] private CurrentInspectableRepositoryProvider currentInspectableRepositoryProvider;
        [AutoLink] [SerializeField] private MapEditingServiceProvider mapEditingServiceProvider;
        [SerializeField] private MapRepositoryProvider mapRepositoryProvider;


        public ConstructInspectorViewModel Create() =>
            new ConstructInspectorViewModel(
                currentInspectableRepositoryProvider.Provide(),
                mapRepositoryProvider.Provide(),
                mapEditingServiceProvider.Provide()
            );
    }
}