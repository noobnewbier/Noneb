using InGameEditor.Data.Availables;
using UnityEngine;

namespace InGameEditor.Data
{
    [CreateAssetMenu(fileName = nameof(EditorPalette), menuName = "Data/InGameEditor/EditorPalette")]
    public class EditorPalette : ScriptableObject
    {
        [SerializeField] private AvailableTileData availableTileData;
        [SerializeField] private AvailableTileRepresentationProviders availableTileRepresentationProviders;
        [SerializeField] private AvailableConstructData availableConstructData;
        [SerializeField] private AvailableConstructRepresentationProviders availableConstructRepresentationProviders;
        [SerializeField] private AvailableUnitData availableUnitData;
        [SerializeField] private AvailableUnitRepresentationProviders availableUnitRepresentationProviders;

        public AvailableTileData AvailableTileData => availableTileData;
        public AvailableTileRepresentationProviders AvailableTileRepresentationProviders => availableTileRepresentationProviders;
        public AvailableConstructData AvailableConstructData => availableConstructData;
        public AvailableConstructRepresentationProviders AvailableConstructRepresentationProviders => availableConstructRepresentationProviders;
        public AvailableUnitData AvailableUnitData => availableUnitData;
        public AvailableUnitRepresentationProviders AvailableUnitRepresentationProviders => availableUnitRepresentationProviders;
    }
}