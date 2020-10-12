using System.Collections.Generic;
using Main.Core.Game.Coordinates;
using Main.Core.Game.GameState.BoardItems;
using Main.Core.Game.GameState.CurrentMapConfig;
using Main.Core.Game.GameState.Maps;
using Main.Core.Game.Maps;
using Main.Core.Game.Tiles;
using Moq;
using NUnit.Framework;
using UniRx;

namespace Core.Game.Tests.GameState.Maps
{
    [TestFixture]
    public class MapRepositoryTests
    {
        private MapRepository _mapRepository;
        private Mock<ICurrentMapConfigRepository> _mockConfigRepository;
        private Mock<IBoardItemsRepository<Tile>> _mockTilesRepository;

        [SetUp]
        public void SetUp()
        {
            _mockConfigRepository = new Mock<ICurrentMapConfigRepository>();
            _mockTilesRepository = new Mock<IBoardItemsRepository<Tile>>();
            _mapRepository = new MapRepository(_mockConfigRepository.Object, _mockTilesRepository.Object);
        }

        [Test]
        public void WhenHasConfigAndTiles_RepositoryReturnConfigWithThoseValues()
        {
            var mapConfig = MapConfig.Create(1, 1);
            var coordinate = new Coordinate(0, 0);
            var tile = new Tile(new TileData(null, string.Empty, null), coordinate);
            var tiles = new[] {tile};
            var expectedValue = new Map(tiles, mapConfig);
            Map returnedValue = null;

            _mockConfigRepository
                .Setup(r => r.GetObservableStream())
                .Returns(Observable.Return(mapConfig));

            _mockTilesRepository
                .Setup(r => r.GetObservableStream())
                .Returns(Observable.Return(tiles));

            _mapRepository.GetObservableStream()
                .SubscribeOn(Scheduler.Immediate)
                .ObserveOn(Scheduler.Immediate)
                .Subscribe(v => returnedValue = v);

            //We know they are the same if the only thing in the map is the same. slightly hacky, but works for now
            Assert.That(returnedValue.Get(coordinate), Is.EqualTo(expectedValue.Get(coordinate)));
        }

        [Test]
        public void WhenHasConfigButNotTiles_RepositoryDoesNotUpdateDownStream()
        {
            var mapConfig = MapConfig.Create(1, 1);
            Map returnedValue = null;

            _mockConfigRepository
                .Setup(r => r.GetObservableStream())
                .Returns(Observable.Return(mapConfig));

            _mockTilesRepository
                .Setup(r => r.GetObservableStream())
                .Returns(Observable.Empty<IReadOnlyList<Tile>>());

            _mapRepository.GetObservableStream()
                .SubscribeOn(Scheduler.Immediate)
                .ObserveOn(Scheduler.Immediate)
                .Subscribe(actualValue => returnedValue = actualValue);

            Assert.That(returnedValue, Is.Null);
        }

        [Test]
        public void WhenOnlyConfigIsUpdated_MapDoesNotUpdate()
        {
            const int expectedUpdateCount = 1;
            var actualUpdateCount = 0;
            var mapConfig = MapConfig.Create(1, 1);
            var tiles = new Tile[] {null};

            _mockConfigRepository
                .Setup(r => r.GetObservableStream())
                .Returns(Observable.Repeat(mapConfig, 2));

            _mockTilesRepository
                .Setup(r => r.GetObservableStream())
                .Returns(Observable.Repeat(tiles, 1));

            _mapRepository.GetObservableStream()
                .SubscribeOn(Scheduler.Immediate)
                .ObserveOn(Scheduler.Immediate)
                .Subscribe(_ => actualUpdateCount++);

            Assert.That(actualUpdateCount, Is.EqualTo(expectedUpdateCount));
        }
        
        [Test]
        public void WhenOnlyTilesIsUpdated_MapDoesNotUpdate()
        {
            const int expectedUpdateCount = 1;
            var actualUpdateCount = 0;
            var mapConfig = MapConfig.Create(1, 1);
            var tiles = new Tile[] {null};

            _mockConfigRepository
                .Setup(r => r.GetObservableStream())
                .Returns(Observable.Repeat(mapConfig, 1));

            _mockTilesRepository
                .Setup(r => r.GetObservableStream())
                .Returns(Observable.Repeat(tiles, 2));

            _mapRepository.GetObservableStream()
                .SubscribeOn(Scheduler.Immediate)
                .ObserveOn(Scheduler.Immediate)
                .Subscribe(_ => actualUpdateCount++);

            Assert.That(actualUpdateCount, Is.EqualTo(expectedUpdateCount));
        }
        
        [Test]
        public void WhenBothConfigAndTilesIsUpdated_MapUpdates()
        {
            const int expectedUpdateCount = 2;
            var actualUpdateCount = 0;
            var mapConfig = MapConfig.Create(1, 1);
            var tiles = new Tile[] {null};

            _mockConfigRepository
                .Setup(r => r.GetObservableStream())
                .Returns(Observable.Repeat(mapConfig, 2));

            _mockTilesRepository
                .Setup(r => r.GetObservableStream())
                .Returns(Observable.Repeat(tiles, 2));

            _mapRepository.GetObservableStream()
                .SubscribeOn(Scheduler.Immediate)
                .ObserveOn(Scheduler.Immediate)
                .Subscribe(_ => actualUpdateCount++);

            Assert.That(actualUpdateCount, Is.EqualTo(expectedUpdateCount));
        }
    }
}