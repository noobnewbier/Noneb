using Common.Factories;
using UnityEngine;
using UnityUtils.Constants;

namespace InGameEditor.Ui.DataSelection.IndividualSelectableData
{
    [CreateAssetMenu(fileName = nameof(SelectableBoardItemDataViewFactory), menuName = MenuName.Providers + nameof(SelectableBoardItemDataView))]
    public class SelectableBoardItemDataViewFactory : PooledMonoBehaviourFactory<SelectableBoardItemDataView>
    {
    }
}