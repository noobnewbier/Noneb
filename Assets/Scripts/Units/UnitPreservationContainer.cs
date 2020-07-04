using Common;
using Constructs;
using JetBrains.Annotations;
using Maps;
using UnityEngine;

namespace Units
{
    public class UnitPreservationContainer : PreservationContainerAsMono<Unit>, IRequireCoordinate
    {
        [SerializeField] private UnitData unitData;
        [SerializeField] private Coordinate coordinate;
        [CanBeNull] [SerializeField] private ConstructPreservationContainer capturedConstructPreservationContainer;

        public Coordinate Coordinate
        {
            set => coordinate = value;
        }

        protected override Unit CreateFromPreservation()
        {
            var construct = capturedConstructPreservationContainer != null ? capturedConstructPreservationContainer.GetPreservation() : null;

            return new Unit(unitData, coordinate, construct);
        }
    }
}