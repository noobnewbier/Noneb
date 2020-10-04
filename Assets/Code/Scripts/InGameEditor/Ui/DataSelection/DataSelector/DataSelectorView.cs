using InGameEditor.Ui.DataSelection.IndividualSelectableData;
using UnityEngine;
using UnityEngine.Serialization;

namespace InGameEditor.Ui.DataSelection.DataSelector
{
    public class DataSelectorView : MonoBehaviour
    {
        [FormerlySerializedAs("selectableBoardItemDataViewProvider")] [SerializeField]
        private SelectableBoardItemDataViewFactory selectableBoardItemDataViewFactory;

        private DataSelectorViewModel _viewModel;
    }
}