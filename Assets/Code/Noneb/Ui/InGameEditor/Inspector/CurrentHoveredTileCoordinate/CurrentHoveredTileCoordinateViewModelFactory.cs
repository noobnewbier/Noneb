﻿using Noneb.Ui.Game.UiState.CurrentHoveredTileHolder;
using UnityEngine;
using UnityUtils.Constants;

namespace Noneb.Ui.InGameEditor.Inspector.CurrentHoveredTileCoordinate
{
    [CreateAssetMenu(
        fileName = nameof(CurrentHoveredTileCoordinateViewModelFactory),
        menuName = MenuName.Factory + nameof(CurrentHoveredTileCoordinateViewModel)
    )]
    public class CurrentHoveredTileCoordinateViewModelFactory : ScriptableObject
    {
        [SerializeField] private CurrentHoveredTileHolderRepositoryProvider repositoryProvider;

        public CurrentHoveredTileCoordinateViewModel Create() => new CurrentHoveredTileCoordinateViewModel(repositoryProvider.Provide());
    }
}