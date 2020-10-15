using Main.Core.Game.Common.Factories;
using UnityEngine;
using UnityUtils.Constants;

namespace Main.Ui.InGameEditor.DataSelection.SelectablePaletteData
{
    [CreateAssetMenu(fileName = nameof(SelectablePaletteDataViewFactory), menuName = MenuName.Factory + nameof(SelectablePaletteDataView))]
    public class SelectablePaletteDataViewFactory : PooledMonoBehaviourFactory<SelectablePaletteDataView>
    {
    }
}