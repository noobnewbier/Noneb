using InGameEditor.Ui.DataSelection.IndividualSelectableData;
using UnityEngine;

namespace InGameEditor.Ui.DataSelection.DataSelector
{
    public class DataSelectorView : MonoBehaviour
    {
        [SerializeField] private SelectableBoardItemDataViewProvider selectableBoardItemDataViewProvider;


        private DataSelectorViewModel _viewModel;
    }
}