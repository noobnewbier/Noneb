using Main.Ui.InGameEditor.DataSelection.IndividualSelectableData;
using UnityEngine;
using UnityEngine.Serialization;

namespace Main.Ui.InGameEditor.DataSelection.DataSelector
{
    public class DataSelectorView : MonoBehaviour
    {
        [FormerlySerializedAs("selectableBoardItemDataViewProvider")] [SerializeField]
        private SelectableBoardItemDataViewFactory selectableBoardItemDataViewFactory;

        private DataSelectorViewModel _viewModel;
    }
}