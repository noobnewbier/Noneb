using Moq;
using Noneb.Core.Game.Constructs;
using Noneb.Core.Game.Coordinates;
using Noneb.Core.Game.GameState.BoardItems;
using Noneb.Core.Game.GameState.MapConfig;
using Noneb.Core.Game.GameState.Maps;
using Noneb.Core.Game.Maps;
using Noneb.Core.Game.Strongholds;
using Noneb.Core.Game.Tiles;
using Noneb.Core.Game.Units;
using NUnit.Framework;
using UniRx;
using Unit = Noneb.Core.Game.Units.Unit;

namespace Core.Game.Tests.GameState.Maps
{
    [TestFixture]
    public class MapRepositoryTests
    {
        [SetUp]
        public void SetUp()
        {
            _mockConfigRepository = new Mock<IMapConfigRepository>();
            _mockTilesRepository = new Mock<IBoardItemsRepository<Tile>>();
            _mockUnitsRepository = new Mock<IBoardItemsGetRepository<Unit>>();
            _mockConstructsRepository = new Mock<IBoardItemsRepository<Construct>>();
            _mockStrongholdsRepository = new Mock<IBoardItemsGetRepository<Stronghold>>();

            _mapRepository = new MapRepository(
                _mockConfigRepository.Object,
                _mockTilesRepository.Object,
                _mockUnitsRepository.Object,
                _mockConstructsRepository.Object,
                _mockStrongholdsRepository.Object
            );
        }

        private MapRepository _mapRepository;
        private Map _expectedMapWithSetUpMock;
        private Mock<IMapConfigRepository> _mockConfigRepository;
        private Mock<IBoardItemsRepository<Tile>> _mockTilesRepository;
        private Mock<IBoardItemsGetRepository<Unit>> _mockUnitsRepository;
        private Mock<IBoardItemsRepository<Construct>> _mockConstructsRepository;
        private Mock<IBoardItemsGetRepository<Stronghold>> _mockStrongholdsRepository;
        
        private void SetUpUpdate(int itemsUpdateCount, int configUpdateCount, int mapXSize = 1, int mapYSize = 1)
        {
            var mapConfig = MapConfig.Create(mapXSize, mapYSize);
            var coordinate = new Coordinate(0, 0);
            var tiles = new[] {new Tile(new TileData(null, string.Empty, null), coordinate)};

            var units = new[] {new Unit(new UnitData(null, string.Empty, null), coordinate)};
            var construct = new Construct(new ConstructData(null, string.Empty, null), coordinate);
            var constructs = new[] {construct};

            var strongholds = new Stronghold[1];

            _mockConfigRepository
                .Setup(r => r.GetObservableStream())
                .Returns(Observable.Repeat(mapConfig, configUpdateCount));

            _mockTilesRepository
                .Setup(r => r.GetObservableStream())
                .Returns(Observable.Repeat(tiles, itemsUpdateCount));

            _mockUnitsRepository
                .Setup(r => r.GetObservableStream())
                .Returns(Observable.Repeat(units, itemsUpdateCount));

            _mockConstructsRepository
                .Setup(r => r.GetObservableStream())
                .Returns(Observable.Repeat(constructs, itemsUpdateCount));

            _mockStrongholdsRepository
                .Setup(r => r.GetObservableStream())
                .Returns(Observable.Repeat(strongholds, itemsUpdateCount));

            _expectedMapWithSetUpMock = new Map(tiles, units, constructs, strongholds, mapConfig);
        }

        [Test]
        public void WhenHasConfigAndItems_RepositoryReturnConfigWithThoseValues()
        {
            var coordinate = new Coordinate(0, 0);
            Map returnedValue = null;

            SetUpUpdate(1, 1);

            _mapRepository.GetObservableStream()
                .SubscribeOn(Scheduler.Immediate)
                .ObserveOn(Scheduler.Immediate)
                .Subscribe(v => returnedValue = v);


            //We know they are the same if the only thing in the map is the same. slightly hacky, but works for now
            Assert.That(returnedValue.Get<Tile>(coordinate), Is.EqualTo(_expectedMapWithSetUpMock.Get<Tile>(coordinate)));
        }
        

        [Test]
        public void WhenOnlyConfigIsUpdated_MapDoesNotUpdate()
        {
            const int expectedUpdateCount = 0;
            var actualUpdateCount = 0;
            
            SetUpUpdate(0,1);

            _mapRepository.GetObservableStream()
                .SubscribeOn(Scheduler.Immediate)
                .ObserveOn(Scheduler.Immediate)
                .Subscribe(_ => actualUpdateCount++);

            Assert.That(actualUpdateCount, Is.EqualTo(expectedUpdateCount));
        }

        [Test]
        public void WhenOnlyItemsIsUpdated_MapDoesNotUpdate()
        {
            const int expectedUpdateCount = 0;
            var actualUpdateCount = 0;
            
            SetUpUpdate(1, 0);

            _mapRepository.GetObservableStream()
                .SubscribeOn(Scheduler.Immediate)
                .ObserveOn(Scheduler.Immediate)
                .Subscribe(_ => actualUpdateCount++);

            Assert.That(actualUpdateCount, Is.EqualTo(expectedUpdateCount));
        }
    }
}