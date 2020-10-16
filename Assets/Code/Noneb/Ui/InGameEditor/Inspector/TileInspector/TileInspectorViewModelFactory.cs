using Noneb.Core.Game.Common.Constants;
using Noneb.Core.Game.Common.Factories;
using Noneb.Ui.InGameEditor.UiState.Inspectable;
using UnityEngine;
using UnityUtils.Constants;

namespace Noneb.Ui.InGameEditor.Inspector.TileInspector
{
    [CreateAssetMenu(fileName = nameof(TileInspectorViewModelFactory), menuName = MenuName.Factory + ProjectMenuName.InGameEditor + nameof(TileInspectorViewModel))]
    public class TileInspectorViewModelFactory : ScriptableObject, IFactory<TileInspectorViewModel>
    {
        [SerializeField] private CurrentInspectableRepositoryProvider currentInspectableRepositoryProvider;
        
        public TileInspectorViewModel Create() => new TileInspectorViewModel(currentInspectableRepositoryProvider.Provide());
    }
}