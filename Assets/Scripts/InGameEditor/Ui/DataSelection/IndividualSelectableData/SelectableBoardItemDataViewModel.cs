using Common.BoardItems;
using Experiment.CrossPlatformLiveData;
using UnityEngine;

namespace InGameEditor.Ui.DataSelection.IndividualSelectableData
{
    public class SelectableBoardItemDataViewModel
    {
        public ILiveData<(Sprite sprite, string dataName)> BoardItemUiInfoLiveData { get; }

        public void OnClick()
        {
            
        }
    }
}