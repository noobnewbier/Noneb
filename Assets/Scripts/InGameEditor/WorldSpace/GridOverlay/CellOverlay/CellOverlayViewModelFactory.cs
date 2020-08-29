﻿using UnityEngine;
using UnityUtils.Constants;

namespace InGameEditor.WorldSpace.GridOverlay.CellOverlay
{
    [CreateAssetMenu(fileName = nameof(CellOverlayViewModelFactory), menuName = MenuName.Factory + nameof(CellOverlayViewModel))]
    public class CellOverlayViewModelFactory : ScriptableObject
    {
        // ReSharper disable once MemberCanBeMadeStatic.Global
        public CellOverlayViewModel Create(bool coordinateVisibility, bool cellVisibility)
        {
            return new CellOverlayViewModel(coordinateVisibility, cellVisibility);
        }
    }
}