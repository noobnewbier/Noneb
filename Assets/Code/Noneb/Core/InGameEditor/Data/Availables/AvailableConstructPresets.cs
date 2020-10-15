using System;
using System.Linq;
using Noneb.Core.Game.Common.Constants;
using Noneb.Core.Game.Common.Factories;
using Noneb.Core.Game.Constructs;
using UnityEngine;
using UnityUtils.Constants;

namespace Noneb.Core.InGameEditor.Data.Availables
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