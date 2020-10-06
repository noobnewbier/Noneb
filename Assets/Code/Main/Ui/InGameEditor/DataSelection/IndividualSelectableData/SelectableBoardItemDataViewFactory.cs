using Main.Core.Game.Common.Factories;
using UnityEngine;
using UnityUtils.Constants;

namespace Main.Ui.InGameEditor.DataSelection.IndividualSelectableData
{
    [CreateAssetMenu(fileName = nameof(SelectableBoardItemDataViewFactory), menuName = MenuName.Providers + nameof(SelectableBoardItemDataView))]
    public class SelectableBoardItemDataViewFactory : PooledMonoBehaviourFactory<SelectableBoardItemDataView>
    {
    }
}