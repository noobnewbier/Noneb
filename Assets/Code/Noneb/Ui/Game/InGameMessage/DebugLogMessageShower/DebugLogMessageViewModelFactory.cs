﻿using Noneb.Core.Game.InGameMessages;
using UnityEngine;
using UnityUtils.Constants;

namespace Noneb.Ui.Game.InGameMessage.DebugLogMessageShower
{
    [CreateAssetMenu(fileName = nameof(DebugLogMessageViewModelFactory), menuName = MenuName.Factory + nameof(DebugLogMessageViewModel))]
    public class DebugLogMessageViewModelFactory : ScriptableObject
    {
        [SerializeField] private InGameMessageServiceProvider messageServiceProvider;

        public IDebugLogMessageViewModel Create() => new DebugLogMessageViewModel(messageServiceProvider.Provide());
    }
}