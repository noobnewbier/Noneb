using System;
using System.Linq;
using Main.Core.Game.Common.Constants;
using Main.Core.Game.Common.Factories;
using Main.Core.Game.Constructs;
using UnityEngine;
using UnityUtils.Constants;

namespace Main.Core.InGameEditor.Data.Availables
{
   [CreateAssetMenu(fileName = nameof(AvailableConstructPresets), menuName = MenuName.Data + ProjectMenuName.InGameEditor + nameof(AvailableConstructPresets))]
    public class AvailableConstructPresets : AvailableDatas<Preset<ConstructData>>
    {
        [SerializeField] private ConstructPresetDataWrapper[] datas;

        public override PaletteData<Preset<ConstructData>>[] Datas => datas.Select(
                d =>
                {
                    var preset = new Preset<ConstructData>(d.ConstructDataScriptable.ToData(), d.GameObjectFactory);

                    return new PaletteData<Preset<ConstructData>>(preset.Data.Icon, preset.Data.Name, preset);
                }
            )
            .ToArray();
        
        
        [Serializable]
        private struct ConstructPresetDataWrapper
        {
            [SerializeField] private ConstructDataScriptable constructDataScriptable;
            [SerializeField] private GameObjectFactory gameObjectFactory;

            public ConstructDataScriptable ConstructDataScriptable => constructDataScriptable;
            public GameObjectFactory GameObjectFactory => gameObjectFactory;
        }
    }
}