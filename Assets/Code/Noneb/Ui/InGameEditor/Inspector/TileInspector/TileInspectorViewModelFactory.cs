using Experiment.NoobAutoLinker.Core;
using Noneb.Core.Game.Common.Constants;
using Noneb.Core.Game.Common.Factories;
using Noneb.Core.Game.GameState.Maps;
using Noneb.Core.Game.Maps.MapModification;
using Noneb.Ui.InGameEditor.UiState.Inspectable;
using UnityEngine;
using UnityUtils.Constants;

namespace Noneb.Ui.InGameEditor.Inspector.TileInspector
{
    [CreateAssetMenu(
        fileName = nameof(TileInspectorViewModelFactory),
        menuName = MenuName.Factory + ProjectMenuName.InGameEditor + "TileInspectorViewModel"
    )]
    public class TileInspectorViewModelFactory : ScriptableObject,
                                                 IFactory<TileInspectorViewModel>
    {
        [SerializeField] private CurrentInspectableRepositoryProvider currentInspectableRepositoryProvider;
        [AutoLink] [SerializeField] private MapEditingServiceProvider mapEditingServiceProvider;
        [SerializeField] private MapRepositoryProvider mapRepositoryProvider;


        public TileInspectorViewModel Create() =>
            new TileInspectorViewModel(
                currentInspectableRepositoryProvider.Provide(),
                mapRepositoryProvider.Provide(),
                mapEditingServiceProvider.Provide()
            );
    }
}