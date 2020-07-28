using InGameEditor.Data.Availables;
using UnityEngine;

namespace InGameEditor.Data
{
    [CreateAssetMenu(fileName = nameof(EditorPalette), menuName = "Data/InGameEditor/EditorPalette")]
    public class EditorPalette : ScriptableObject
    {
        [SerializeField] private AvailableTileData availableTileData;
        [SerializeField] private AvailableGameObjectProviders availableTileGameObjectProviders;
        [SerializeField] private AvailableConstructData availableConstructData;
        [SerializeField] private AvailableGameObjectProviders availableConstructGameObjectProviders;
        [SerializeField] private AvailableUnitData availableUnitData;
        [SerializeField] private AvailableGameObjectProviders availableUnitGameObjectProviders;

        public AvailableTileData AvailableTileData => availableTileData;
        public AvailableGameObjectProviders AvailableTileGameObjectProviders => availableTileGameObjectProviders;
        public AvailableConstructData AvailableConstructData => availableConstructData;
        public AvailableGameObjectProviders AvailableConstructGameObjectProviders => availableConstructGameObjectProviders;
        public AvailableUnitData AvailableUnitData => availableUnitData;
        public AvailableGameObjectProviders AvailableUnitGameObjectProviders => availableUnitGameObjectProviders;
    }
}