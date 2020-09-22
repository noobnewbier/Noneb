using Common.BoardItems;
using Constructs;
using Constructs.Data;
using Units;
using Units.Data;
using UnityEngine;

namespace Strongholds
{
    /// <summary>
    /// Unlike other data class, this is not a scriptable as it should not be created manually,
    /// it should be created at runtime through player's interaction between unit and construct
    /// </summary>
    public class StrongholdData : BoardItemData
    {
        public UnitData UnitData { get; }

        public ConstructData ConstructData { get; }

        private StrongholdData(Sprite icon, string name, ConstructData constructData, UnitData unitData) :
            base(icon, name)
        {
            ConstructData = constructData;
            UnitData = unitData;
        }

        /// <summary>
        /// TODO: Require actual implementation 
        /// need to fix icon param, it should not be units sprite,
        /// but a mix of construct and unit(or whatever implementation you favour),
        /// it's just something to make it compile for now
        /// </summary>
        public static StrongholdData Create(ConstructData constructData, UnitData unitData)
        {
            return new StrongholdData(
                unitData.Icon,
                $"{constructData.Name} captured by : {unitData.Name}",
                constructData,
                unitData
            );
        }
    }
}