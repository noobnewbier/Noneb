using Noneb.Core.Game.Common.Constants;
using Noneb.Core.Game.Common.Factories;
using Noneb.Core.Game.Constructs;
using Noneb.Core.Game.GameState.Maps;
using Noneb.Ui.InGameEditor.UiState.Inspectable;
using UnityEngine;
using UnityUtils.Constants;

namespace Noneb.Ui.InGameEditor.Inspector.ConstructInspector
{
    [CreateAssetMenu(
        fileName = nameof(ConstructInspectorViewModelFactory),
        menuName = MenuName.Factory + ProjectMenuName.InGameEditor + "ConstructInspectorViewModel"
    )]
    public class ConstructInspectorViewModelFactory : ScriptableObject, IFactory<InspectorViewModel<Construct, ConstructData>>
    {
        [SerializeField] private CurrentInspectableRepositoryProvider currentInspectableRepositoryProvider;
        [SerializeField] private MapRepositoryProvider mapRepositoryProvider;


        public InspectorViewModel<Construct, ConstructData> Create() =>
            new InspectorViewModel<Construct, ConstructData>(
                currentInspectableRepositoryProvider.Provide(),
                mapRepositoryProvider.Provide()
            );
    }
}