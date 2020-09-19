using Common;
using Maps;
using UnityEngine;
using UnityEngine.Serialization;

namespace Units
{
    public class UnitPreservationContainer : PreservationContainerAsMono<Unit>, IRequireCoordinate
    {
        [SerializeField] private UnitData unitData;
        [SerializeField] private Coordinate coordinate;

        public Coordinate Coordinate
        {
            set => coordinate = value;
        }

        protected override Unit CreateFromPreservation()
        {
            return new Unit(unitData, coordinate);
        }
    }
}