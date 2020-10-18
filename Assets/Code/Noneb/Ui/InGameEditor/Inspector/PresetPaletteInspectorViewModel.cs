using Noneb.Core.Game.Common;
using Noneb.Core.Game.Common.BoardItems;
using Noneb.Core.InGameEditor.Common;
using Noneb.Core.InGameEditor.Data;

namespace Noneb.Ui.InGameEditor.Inspector
{
    public class PresetPaletteInspectorViewModel<TPaletteData, TConcernedData> : InspectorViewModelBase<TConcernedData>
        where TPaletteData : PaletteData<Preset<TConcernedData>>
        where TConcernedData : BoardItemData
    {
        public PresetPaletteInspectorViewModel(IDataGetRepository<IInspectable> currentInspectableGetRepository) : base(
            currentInspectableGetRepository
        )
        {
        }


        protected override bool TryGetTFromInspectable(IInspectable inspectable, out TConcernedData t)
        {
            if (inspectable is TPaletteData paletteData)
            {
                t = paletteData.Data.Data;
                return true;
            }
            
            t = default;
            return false;
        }
    }
}