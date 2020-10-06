using Experiment.CrossPlatformLiveData;
using UnityEngine;

namespace Main.Ui.InGameEditor.DataSelection.IndividualSelectableData
{
    public class SelectableBoardItemDataViewModel
    {
        public ILiveData<(Sprite sprite, string dataName)> BoardItemUiInfoLiveData { get; }

        public void OnClick()
        {
        }
    }
}