using Noneb.Core.Game.Common.Constants;
using Noneb.Core.Game.Common.Factories;
using Noneb.Core.InGameEditor.Data;
using Noneb.Ui.InGameEditor.UiState.Inspectable;
using UnityEngine;
using UnityUtils.Constants;

namespace Noneb.Ui.InGameEditor.DataSelection.SelectablePaletteData
{
    [CreateAssetMenu(
        fileName = nameof(SelectablePaletteDataViewModelFactory),
        menuName = MenuName.Factory + ProjectMenuName.InGameEditor + "SelectablePaletteDataViewModel"
    )]
    internal class SelectablePaletteDataViewModelFactory : ScriptableObject,
                                                           IFactory<PaletteData, Sprite, string, SelectablePaletteDataViewModel<PaletteData>>
    {
        [SerializeField] private CurrentInspectableRepositoryProvider currentInspectableRepositoryProvider;

        public SelectablePaletteDataViewModel<PaletteData> Create(PaletteData arg1, Sprite arg2, string arg3) =>
            new SelectablePaletteDataViewModel<PaletteData>(arg1, arg2, arg3, currentInspectableRepositoryProvider.Provide());
    }
}