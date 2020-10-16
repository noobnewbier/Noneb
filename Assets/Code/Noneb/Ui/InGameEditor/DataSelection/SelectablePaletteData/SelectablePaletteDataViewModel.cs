using Experiment.CrossPlatformLiveData;
using Noneb.Core.Game.Common;
using Noneb.Core.InGameEditor.Common;
using Noneb.Core.InGameEditor.Data;
using UnityEngine;

namespace Noneb.Ui.InGameEditor.DataSelection.SelectablePaletteData
{

    public class SelectablePaletteDataViewModel<T> where T: PaletteData
    {
        public ILiveData<(Sprite sprite, string dataName)> BoardItemUiInfoLiveData { get; }
        private readonly T _paletteData;
        private readonly IDataSetRepository<IInspectable> _currentInspectableRepository;

        public SelectablePaletteDataViewModel(T paletteData,
                                              Sprite sprite,
                                              string dataName,
                                              IDataSetRepository<IInspectable> currentInspectableRepository)
        {
            BoardItemUiInfoLiveData = new LiveData<(Sprite sprite, string dataName)>();
            _paletteData = paletteData;
            _currentInspectableRepository = currentInspectableRepository;

            BoardItemUiInfoLiveData.PostValue((sprite, dataName));
        }

        public void Inspect()
        {
            _currentInspectableRepository.Set(_paletteData);
        }
    }
}