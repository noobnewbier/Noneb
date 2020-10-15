using System;
using System.Linq;
using Noneb.Core.Game.Common.Constants;
using Noneb.Core.Game.Common.Factories;
using Noneb.Core.Game.Tiles;
using UnityEngine;
using UnityUtils.Constants;

namespace Noneb.Core.InGameEditor.Data.Availables
{
    [CreateAssetMenu(fileName = nameof(AvailableTilePresets), menuName = MenuName.Data + ProjectMenuName.InGameEditor + nameof(AvailableTilePresets))]
    public class AvailableTilePresets : AvailableDatas<Preset<TileData>>
    {
        [SerializeField] private TilePresetDataWrapper[] datas;

        public override PaletteData<Preset<TileData>>[] Datas => datas.Select(
                d =>
                {
                    var preset = new Preset<TileData>(d.TileDataScriptable.ToData(), d.GameObjectFactory);

                    return new PaletteData<Preset<TileData>>(preset.Data.Icon, preset.Data.Name, preset);
                }
            )
            .ToArray();
        
        
        [Serializable]
        private struct TilePresetDataWrapper
        {
            [SerializeField] private TileDataScriptable tileDataScriptable;
            [SerializeField] private GameObjectFactory gameObjectFactory;

            public TileDataScriptable TileDataScriptable => tileDataScriptable;
            public GameObjectFactory GameObjectFactory => gameObjectFactory;
        }
    }
}