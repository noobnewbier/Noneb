﻿using Common.BoardItems;
using UnityEngine;

namespace Units.Data
{
    public class UnitData : BoardItemData
    {
        public UnitData(Sprite icon, string name, UnitDataScriptable original) : base(icon, name)
        {
            Original = original;
        }

        public UnitDataScriptable Original { get; }

        public float MaxHealth => Original.MaxHealth;
        public float Health => Original.Health;
    }
}