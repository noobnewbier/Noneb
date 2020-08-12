using InGameEditor.Data.Availables;
using UnityEngine;
using UnityEngine.Serialization;

namespace InGameEditor.Data
{
    [CreateAssetMenu(fileName = nameof(EditorPalette), menuName = "Data/InGameEditor/EditorPalette")]
    public class EditorPalette : ScriptableObject
    {
        [FormerlySerializedAs("availableTileData")] [SerializeField]
        private AvailableTileDatas availableTileDatas;

        [SerializeField] private AvailableGameObjectProviders availableTileGameObjectProviders;

        [FormerlySerializedAs("availableConstructData")] [SerializeField]
        private AvailableConstructDatas availableConstructDatas;

        [SerializeField] private AvailableGameObjectProviders availableConstructGameObjectProviders;

        [FormerlySerializedAs("availableUnitData")] [SerializeField]
        private AvailableUnitDatas availableUnitDatas;

        [SerializeField] private AvailableGameObjectProviders availableUnitGameObjectProviders;

        public AvailableTileDatas AvailableTileDatas => availableTileDatas;
        public AvailableGameObjectProviders AvailableTileGameObjectProviders => availableTileGameObjectProviders;
        public AvailableConstructDatas AvailableConstructDatas => availableConstructDatas;
        public AvailableGameObjectProviders AvailableConstructGameObjectProviders => availableConstructGameObjectProviders;
        public AvailableUnitDatas AvailableUnitDatas => availableUnitDatas;
        public AvailableGameObjectProviders AvailableUnitGameObjectProviders => availableUnitGameObjectProviders;
    }
}