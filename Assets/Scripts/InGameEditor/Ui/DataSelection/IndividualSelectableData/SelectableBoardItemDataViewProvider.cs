using Common.Providers;
using UnityEditor;
using UnityEngine;
using UnityUtils.Constants;

namespace InGameEditor.Ui.DataSelection.IndividualSelectableData
{
    [CreateAssetMenu(fileName = nameof(SelectableBoardItemDataViewProvider), menuName = MenuName.Providers + nameof(SelectableBoardItemDataView))]
    public class SelectableBoardItemDataViewProvider : PooledMonoBehaviourProvider<SelectableBoardItemDataView>
    {
        
    }
}