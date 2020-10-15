﻿using System;
using System.Linq;
using Main.Core.Game.Common.Constants;
using Main.Core.Game.Common.Factories;
using Main.Core.Game.Units;
using UnityEngine;
using UnityUtils.Constants;

namespace Main.Core.InGameEditor.Data.Availables
{
    [CreateAssetMenu(fileName = nameof(AvailableUnitPresets), menuName = MenuName.Data + ProjectMenuName.InGameEditor + nameof(AvailableUnitPresets))]
    public class AvailableUnitPresets : AvailableDatas<Preset<UnitData>>
    {
        [SerializeField] private UnitPresetDataWrapper[] datas;

        public override PaletteData<Preset<UnitData>>[] Datas => datas.Select(
                d =>
                {
                    var preset = new Preset<UnitData>(d.UnitDataScriptable.ToData(), d.GameObjectFactory);

                    return new PaletteData<Preset<UnitData>>(preset.Data.Icon, preset.Data.Name, preset);
                }
            )
            .ToArray();
        
        
        [Serializable]
        private struct UnitPresetDataWrapper
        {
            [SerializeField] private UnitDataScriptable unitDataScriptable;
            [SerializeField] private GameObjectFactory gameObjectFactory;

            public UnitDataScriptable UnitDataScriptable => unitDataScriptable;
            public GameObjectFactory GameObjectFactory => gameObjectFactory;
        }
    }
}