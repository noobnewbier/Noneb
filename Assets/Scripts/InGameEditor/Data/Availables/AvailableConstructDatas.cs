﻿using Constructs;
using UnityEngine;
using UnityUtils.Constants;

namespace InGameEditor.Data.Availables
{
    [CreateAssetMenu(fileName = nameof(AvailableConstructDatas), menuName = MenuName.Data + "InGameEditor/" + nameof(AvailableConstructDatas))]
    public class AvailableConstructDatas : AvailableSet<ConstructData>
    {
    }
}