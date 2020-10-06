﻿using UnityEngine;

namespace Main.Core.Game.Common.BoardItems
{
    public abstract class BoardItemDataScriptable : ScriptableObject
    {
        [SerializeField] private Sprite icon;
        protected Sprite Icon => icon;
    }
}