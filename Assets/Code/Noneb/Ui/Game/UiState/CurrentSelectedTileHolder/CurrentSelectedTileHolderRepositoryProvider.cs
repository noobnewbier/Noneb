﻿using System;
using Noneb.Core.Game.Common.Providers;
using UnityEngine;
using UnityUtils.Constants;

namespace Noneb.Ui.Game.UiState.CurrentSelectedTileHolder
{
    [CreateAssetMenu(
        fileName = nameof(CurrentSelectedTileHolderRepositoryProvider),
        menuName = MenuName.ScriptableRepository + nameof(CurrentSelectedTileHolderRepository)
    )]
    public class CurrentSelectedTileHolderRepositoryProvider : ScriptableObject, IObjectProvider<CurrentSelectedTileHolderRepository>
    {
        private readonly Lazy<CurrentSelectedTileHolderRepository> _lazyInstance =
            new Lazy<CurrentSelectedTileHolderRepository>(() => new CurrentSelectedTileHolderRepository());

        public CurrentSelectedTileHolderRepository Provide() => _lazyInstance.Value;
    }
}