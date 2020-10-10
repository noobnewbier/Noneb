using Main.Core.Game.Coordinates;
using Main.Core.Game.Maps;
using NUnit.Framework;

namespace Core.Tests.Game.Coordinates
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

                Assert.AreEqual(expectedAnswer, returnedValue);
            }

            [Test]
            public void Given_X0_Z0_ReturnCorrectAnswer_X0_Z0()
            {
                var expectedAnswer = new Coordinate(0, 0);

                var returnedValue = _coordinateService.GetAxialCoordinateFromNestedArrayIndex(0, 0);

                Assert.AreEqual(expectedAnswer, returnedValue);
            }
        }

        public class GetCoordinateFromFlattenArrayIndex
        {
            private CoordinateService _coordinateService;
            private MapConfig _threeByThreeConfig;

            [SetUp]
            public void SetUp()
            {
                _coordinateService = new CoordinateService();
                _threeByThreeConfig = MapConfig.Create(3, 3);
            }

            [Test]
            public void Given_Param1As8_Param2As3x3_ReturnCorrectAnswer_X3_Z2()
            {
                var expectedAnswer = new Coordinate(3, 2);

                var returnedValue = _coordinateService.GetCoordinateFromFlattenArrayIndex(8, _threeByThreeConfig);

                Assert.AreEqual(expectedAnswer, returnedValue);
            }

            [Test]
            public void Given_Param1As0_Param2As3x3_ReturnCorrectAnswer_X0_Z0()
            {
                var expectedAnswer = new Coordinate(0, 0);

                var returnedValue = _coordinateService.GetCoordinateFromFlattenArrayIndex(0, _threeByThreeConfig);

                Assert.AreEqual(expectedAnswer, returnedValue);
            }
        }
    }
}