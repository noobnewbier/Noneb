﻿using Common;
using JetBrains.Annotations;
using Maps;
using Tiles.Data;
using Units;
using UnityEngine;

namespace Tiles
{
    public class TilePreservationContainer : PreservationContainerAsMono<Tile>, IRequireCoordinate
    {
        [SerializeField] private Coordinate coordinate;
        [SerializeField] [CanBeNull] private UnitPreservationContainer unitPreservationContainer;
        [SerializeField] private TileData tileData;

        public Coordinate Coordinate
        {
            set => coordinate = value;
        }

        protected override Tile CreateFromPreservation()
        {
            var unit = unitPreservationContainer != null ? unitPreservationContainer.GetPreservation() : null;

            return new Tile(tileData, coordinate);
        }
    }
}