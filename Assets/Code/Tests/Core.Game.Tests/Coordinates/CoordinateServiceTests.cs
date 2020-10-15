using System;
using System.Collections.Generic;
using Noneb.Core.Game.Coordinates;
using Noneb.Core.Game.Maps;
using NUnit.Framework;

namespace Core.Game.Tests.Coordinates
{
    [TestFixture]
    public class CoordinateServiceTests
    {
        public class GetAxialCoordinateFromNestedArrayIndex
        {
            private CoordinateService _coordinateService;

            [SetUp]
            public void SetUp()
            {
                _coordinateService = new CoordinateService();
            }

            [Test]
            public void Given_X2_Z2_ReturnCorrectAnswer_X3_Z2()
            {
                var expectedAnswer = new Coordinate(3, 2);

                var returnedValue = _coordinateService.GetAxialCoordinateFromNestedArrayIndex(2, 2);

                Assert.That(returnedValue, Is.EqualTo(expectedAnswer));
            }

            [Test]
            public void Given_X0_Z0_ReturnCorrectAnswer_X0_Z0()
            {
                var expectedAnswer = new Coordinate(0, 0);

                var returnedValue = _coordinateService.GetAxialCoordinateFromNestedArrayIndex(0, 0);

                Assert.That(returnedValue, Is.EqualTo(expectedAnswer));
            }
        }

        public class GetCoordinateFromFlattenArrayIndex
        {
            private CoordinateService _coordinateService;
            private MapConfig _3X3Config;

            [SetUp]
            public void SetUp()
            {
                _coordinateService = new CoordinateService();
                _3X3Config = MapConfig.Create(3, 3);
            }

            [Test]
            public void Given_Param1As8_Param2As3x3_ReturnCorrectAnswer_X3_Z2()
            {
                var expectedAnswer = new Coordinate(3, 2);

                var returnedValue = _coordinateService.GetCoordinateFromFlattenArrayIndex(8, _3X3Config);

                Assert.That(returnedValue, Is.EqualTo(expectedAnswer));
            }

            [Test]
            public void Given_Param1As0_Param2As3x3_ReturnCorrectAnswer_X0_Z0()
            {
                var expectedAnswer = new Coordinate(0, 0);

                var returnedValue = _coordinateService.GetCoordinateFromFlattenArrayIndex(0, _3X3Config);

                Assert.That(returnedValue, Is.EqualTo(expectedAnswer));
            }

            [Test]
            public void WhenIndexOutOfRange_ThrowArgumentOutOfRangeException()
            {
                var outOfRangeIndex = _3X3Config.GetTotalMapSize() + 1;

                void CallWithIndexOutOfRange()
                {
                    _coordinateService.GetCoordinateFromFlattenArrayIndex(outOfRangeIndex, _3X3Config);
                }

                Assert.That(CallWithIndexOutOfRange, Throws.TypeOf<ArgumentOutOfRangeException>());
            }

            [Test]
            public void WhenIndexNegative_ThrowArgumentOutOfRangeException()
            {
                const int negativeIndex = -1;

                void CallWithNegativeIndex()
                {
                    _coordinateService.GetCoordinateFromFlattenArrayIndex(negativeIndex, _3X3Config);
                }

                Assert.That(CallWithNegativeIndex, Throws.TypeOf<ArgumentOutOfRangeException>());
            }
        }

        public class GetFlattenArrayIndexFromAxialCoordinate
        {
            private CoordinateService _coordinateService;
            private MapConfig _3X3Config;

            [SetUp]
            public void SetUp()
            {
                _coordinateService = new CoordinateService();
                _3X3Config = MapConfig.Create(3, 3);
            }

            [Test]
            public void Given_X0_Z0_ReturnCorrectAnswer_0()
            {
                const int expectedAnswer = 0;

                var returnedValue = _coordinateService.GetFlattenArrayIndexFromAxialCoordinate(0, 0, _3X3Config);

                Assert.That(returnedValue, Is.EqualTo(expectedAnswer));
            }

            [Test]
            public void Given_X3_Z2_ReturnCorrectAnswer_8()
            {
                const int expectedAnswer = 8;

                var returnedValue = _coordinateService.GetFlattenArrayIndexFromAxialCoordinate(3, 2, _3X3Config);

                Assert.That(returnedValue, Is.EqualTo(expectedAnswer));
            }

            [Test]
            public void WhenXIsOutOfRange_ThrowsArgumentOutOfRangeException()
            {
                var outOfRangeX = _3X3Config.GetMap2DArrayWidth() + 1;
                const int withinRangeZ = 0;

                void CallWithXOutOfRange()
                {
                    _coordinateService.GetFlattenArrayIndexFromAxialCoordinate(
                        outOfRangeX,
                        withinRangeZ,
                        _3X3Config
                    );
                }

                Assert.That(CallWithXOutOfRange, Throws.TypeOf<ArgumentOutOfRangeException>());
            }

            [Test]
            public void WhenZIsOutOfRange_ThrowsArgumentOutOfRangeException()
            {
                var outOfRangeZ = _3X3Config.GetMap2DArrayHeight() + 1;
                const int withinRangeX = 0;

                void CallWithXOutOfRange()
                {
                    _coordinateService.GetFlattenArrayIndexFromAxialCoordinate(
                        withinRangeX,
                        outOfRangeZ,
                        _3X3Config
                    );
                }

                Assert.That(CallWithXOutOfRange, Throws.TypeOf<ArgumentOutOfRangeException>());
            }
        }

        public class GetFlattenCoordinates
        {
            private CoordinateService _coordinateService;
            private MapConfig _3X3Config;

            [SetUp]
            public void SetUp()
            {
                _coordinateService = new CoordinateService();
                _3X3Config = MapConfig.Create(3, 3);
            }

            [Test]
            public void Given3x3Config_ReturnCorrectAnswer()
            {
                var expectedAnswer = new List<Coordinate>
                {
                    new Coordinate(0, 0), new Coordinate(1, 0), new Coordinate(2, 0),
                    new Coordinate(1, 1), new Coordinate(2, 1), new Coordinate(3, 1),
                    new Coordinate(1, 2), new Coordinate(2, 2), new Coordinate(3, 2)
                };

                var returnedValue = _coordinateService.GetFlattenCoordinates(_3X3Config);

                Assert.That(returnedValue, Is.EquivalentTo(expectedAnswer));
            }

            [Test]
            public void Given0x0Config_ReturnEmptyList()
            {
                var expectedAnswer = new Coordinate[0];

                var returnedValue = _coordinateService.GetFlattenCoordinates(MapConfig.Empty);
                
                Assert.That(returnedValue, Is.EquivalentTo(expectedAnswer));
            }
        }
    }
}