using Experiment.CrossPlatformLiveData;
using UnityEngine;

namespace Noneb.Ui.InGameEditor.DataSelection.SelectablePaletteData
{
    public class SelectablePaletteDataViewModel
    {
        public SelectablePaletteDataViewModel(Sprite sprite, string dataName)
        {
            BoardItemUiInfoLiveData = new LiveData<(Sprite sprite, string dataName)>();

            BoardItemUiInfoLiveData.PostValue((sprite, dataName));
        }

        public ILiveData<(Sprite sprite, string dataName)> BoardItemUiInfoLiveData { get; }
    }
}