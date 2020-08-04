﻿using System;
using Common.Providers;
using UnityEngine;
using UnityUtils.Constants;

namespace InGameEditor.WorldSpace.Maps
{
    [CreateAssetMenu(fileName = nameof(MapViewModelProvider), menuName = MenuName.Providers + "ProvidersMapViewModel")]
    public class MapViewModelProvider : ScriptableObjectProvider<MapViewModel>
    {
        public override MapViewModel Provide()
        {
            throw new NotImplementedException();
        }
    }
}