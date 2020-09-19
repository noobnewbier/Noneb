﻿using Common.Providers;
using Maps.Repositories.CurrentMapConfig;
using Tiles.Holders.Repository;
using UnityEngine;
using UnityEngine.Serialization;

namespace Maps.Repositories.Map
{
    public class MapRepositoryProvider : MonoObjectProvider<IMapRepository>
    {
        [FormerlySerializedAs("mapConfigurationRepositoryProvider")] [SerializeField]
        private CurrentMapConfigRepositoryProvider currentMapConfigRepositoryProvider;

        [FormerlySerializedAs("tileHolderRepositoryProvider")] [SerializeField]
        private TileHoldersRepositoryProvider tileHoldersRepositoryProvider;

        private IMapRepository _cache;

        public override IMapRepository Provide()
        {
            return _cache ?? (_cache = new MapRepository(currentMapConfigRepositoryProvider.Provide(), tileHoldersRepositoryProvider.Provide()));
        }
    }
}