using Common;
using Constructs;
using Maps;
using Units;
using UnityEngine;

namespace Strongholds
{
    public class StrongholdPreservationContainer :PreservationContainerAsMono<Stronghold>, IRequireCoordinate
    {
        [SerializeField] private UnitPreservationContainer unitPreservationContainer;
        [SerializeField] private ConstructPreservationContainer constructPreservationContainer;
        [SerializeField] private Coordinate coordinate;
        
        
        protected override Stronghold CreateFromPreservation()
        {
            var unit = unitPreservationContainer.GetPreservation();
            var construct = constructPreservationContainer.GetPreservation();

            return new Stronghold(unit, construct, coordinate);
        }

        public Coordinate Coordinate
        {
            set
            {
                unitPreservationContainer.Coordinate = value;
                constructPreservationContainer.Coordinate = value;
                coordinate = value;
            }
        }
    }
}