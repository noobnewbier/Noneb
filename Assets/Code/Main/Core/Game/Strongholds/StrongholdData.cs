using Main.Core.Game.Common.BoardItems;
using Main.Core.Game.Constructs;
using Main.Core.Game.Units;
using UnityEngine;

namespace Main.Core.Game.Strongholds
{
    public class StrongholdData : BoardItemData
    {
        private StrongholdData(Sprite icon, string name, ConstructData constructData, UnitData unitData) :
            base(icon, name)
        {
            ConstructData = constructData;
            UnitData = unitData;
        }

        public UnitData UnitData { get; }

        public ConstructData ConstructData { get; }

        /// <summary>
        /// TODO: Require actual implementation
        /// need to fix icon param, it should not be units sprite,
        /// but a mix of construct and unit(or whatever implementation you favour),
        /// it's just something to make it compile for now
        /// </summary>
        public static StrongholdData Create(ConstructData constructData, UnitData unitData) =>
            new StrongholdData(
                unitData.Icon,
                $"{constructData.Name} captured by : {unitData.Name}",
                constructData,
                unitData
            );
    }
}