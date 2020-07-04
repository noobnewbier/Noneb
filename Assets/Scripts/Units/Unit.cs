using System;
using Constructs;
using JetBrains.Annotations;
using Maps;

namespace Units
{
    public class Unit
    {
        public UnitData UnitData { get; }
        public Coordinate Coordinate { get; private set; }
        [CanBeNull] public Construct CapturedConstruct { get; private set; }

        public Unit(UnitData unitData, Coordinate coordinate, [CanBeNull] Construct capturedConstruct)
        {
            UnitData = unitData;
            Coordinate = coordinate;
            CapturedConstruct = capturedConstruct;
        }

        public void MoveTo(Coordinate coordinate)
        {
            Coordinate = coordinate;
        }

        public void Capture(Construct construct)
        {
            CapturedConstruct = construct;
        }

        public void Abandon()
        {
            if (CapturedConstruct  != null)
            {
                throw new InvalidOperationException("The unit is not currently occupying a construct");
            }
            
            CapturedConstruct = null;
        }
    }
}