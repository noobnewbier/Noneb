using Experiment.NoobAutoLinker.Core;
using Noneb.Core.Game.Common.Constants;
using Noneb.Core.Game.Common.Factories;
using Noneb.Core.Game.GameState.Maps;
using Noneb.Core.Game.InGameMessages;
using Noneb.Core.Game.Maps.MapModification;
using Noneb.Core.InGameEditor.LevelEdit;
using Noneb.Ui.InGameEditor.UiState.Inspectable;
using UnityEngine;
using UnityEngine.Serialization;
using UnityUtils.Constants;

namespace Noneb.Ui.InGameEditor.Inspector.StrongholdInspector
{
    [CreateAssetMenu(
        fileName = nameof(StrongholdInspectorViewModelFactory),
        menuName = MenuName.Factory + ProjectMenuName.InGameEditor + nameof(StrongholdInspectorViewModel)
    )]
    public class StrongholdInspectorViewModelFactory : ScriptableObject, IFactory<StrongholdInspectorViewModel>
    {
        [SerializeField] private CurrentInspectableRepositoryProvider currentInspectableRepositoryProvider;
        [SerializeField] private MapRepositoryProvider mapRepositoryProvider;

        [FormerlySerializedAs("levelEditServiceProvider")] [SerializeField]
        private LevelEditingServiceProvider levelEditingServiceProvider;

        [SerializeField] private InGameMessageServiceProvider inGameMessageServiceProvider;
        [AutoLink] [SerializeField] private MapEditingServiceProvider mapEditingServiceProvider;


        public StrongholdInspectorViewModel Create() => new StrongholdInspectorViewModel(
            currentInspectableRepositoryProvider.Provide(),
            mapRepositoryProvider.Provide(),
            levelEditingServiceProvider.Provide(),
            inGameMessageServiceProvider.Provide(),
            mapEditingServiceProvider.Provide()
        );
    }
}