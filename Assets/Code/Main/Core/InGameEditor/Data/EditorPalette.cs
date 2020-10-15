using Main.Core.Game.Common.Constants;
using Main.Core.InGameEditor.Data.Availables;
using UnityEngine;
using UnityEngine.Serialization;
using UnityUtils.Constants;

namespace Main.Core.InGameEditor.Data
{
    [CreateAssetMenu(fileName = nameof(EditorPalette), menuName = MenuName.Data + ProjectMenuName.InGameEditor + nameof(EditorPalette))]
    public class EditorPalette : ScriptableObject
    {
        [FormerlySerializedAs("availableTileDatas")] [SerializeField] private AvailableTilePresets availableTilePresets;
        [FormerlySerializedAs("availableConstructDatas")] [SerializeField] private AvailableConstructPresets availableConstructPresets;
        [FormerlySerializedAs("availableUnitDatas")] [SerializeField] private AvailableUnitPresets availableUnitPresets;

        public AvailableTilePresets AvailableTilePresets => availableTilePresets;
        public AvailableConstructPresets AvailableConstructPresets => availableConstructPresets;
        public AvailableUnitPresets AvailableUnitPresets => availableUnitPresets;
    }
}