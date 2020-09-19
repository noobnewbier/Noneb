﻿using Common.BoardItems;
using Constructs;
using Units;
using UnityEngine;

namespace Strongholds
{
    /// <summary>
    /// Unlike other data class, this is not a scriptable as it should not be created manually,
    /// it should be created at runtime through player's interaction between unit and construct
    /// </summary>
    public class StrongholdData : BoardItemData
    {
        public StrongholdData(UnitData unitData, ConstructData constructData)
        {
            UnitData = unitData;
            ConstructData = constructData;
        }

        public UnitData UnitData { get; }

        public ConstructData ConstructData { get; }
        public override string DataName => $"{ConstructData.DataName} captured by : {UnitData.DataName}";
    }
}