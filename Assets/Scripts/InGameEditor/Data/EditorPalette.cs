using InGameEditor.Data.Availables;
using UnityEngine;
using UnityUtils.Constants;

namespace InGameEditor.Data
{
    [CreateAssetMenu(fileName = nameof(EditorPalette), menuName = MenuName.Data + "/InGameEditor/EditorPalette")]
    public class EditorPalette : ScriptableObject
    {
        [SerializeField] private AvailableTileDatas availableTileDatas;
        [SerializeField] private AvailableGameObjectProviders availableTileGameObjectProviders;
        [SerializeField] private AvailableConstructDatas availableConstructDatas;
        [SerializeField] private AvailableGameObjectProviders availableConstructGameObjectProviders;
        [SerializeField] private AvailableUnitDatas availableUnitDatas;
        [SerializeField] private AvailableGameObjectProviders availableUnitGameObjectProviders;

        public AvailableTileDatas AvailableTileDatas => availableTileDatas;
        public AvailableGameObjectProviders AvailableTileGameObjectProviders => availableTileGameObjectProviders;
        public AvailableConstructDatas AvailableConstructDatas => availableConstructDatas;
        public AvailableGameObjectProviders AvailableConstructGameObjectProviders => availableConstructGameObjectProviders;
        public AvailableUnitDatas AvailableUnitDatas => availableUnitDatas;
        public AvailableGameObjectProviders AvailableUnitGameObjectProviders => availableUnitGameObjectProviders;
    }
}