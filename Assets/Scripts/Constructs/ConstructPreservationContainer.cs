using Common;
using Maps;
using UnityEngine;
using UnityEngine.Serialization;

namespace Constructs
{
    public class ConstructPreservationContainer : PreservationContainerAsMono<Construct>, IRequireCoordinate
    {
        [SerializeField] private Coordinate coordinate;
        [SerializeField] private ConstructData constructData;

        public Coordinate Coordinate
        {
            set => coordinate = value;
        }

        protected override Construct CreateFromPreservation()
        {
            return new Construct(constructData, coordinate);
        }
    }
}