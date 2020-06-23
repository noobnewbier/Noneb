﻿using Common;
using UnityEngine;

namespace Tiles
{
    [CreateAssetMenu(menuName = "Providers/TileRepresentation", fileName = "TileRepresentationProvider")]
    public class TileRepresentationProvider : ScriptableObjectProvider<TileRepresentation>
    {
        [SerializeField] private GameObject prefab;

        public override TileRepresentation Provide()
        {
            return Instantiate(prefab).GetComponent<TileRepresentation>();
        }
    }
}