using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Moq;
using Noneb.Core.Game.Common.BoardItems;
using Noneb.Core.Game.Common.Factories;
using Noneb.Core.Game.Coordinates;
using Noneb.Core.Game.GameEnvironments.Load;
using Noneb.Core.Game.GameState.BoardItems;
using NUnit.Framework;
using UnityEngine;

namespace Core.Game.Tests.GameEnvironments.Load
{
    [TestFixture]
    public class LoadBoardItemsServiceTests
    {
        private LoadBoardItemsService<DummyBoardItem, DummyBoardItemData> _loadService;
        private Mock<IBoardItemsRepository<DummyBoardItem>> _itemRepositoryMock;
        private Mock<IFactory<DummyBoardItemData, Coordinate, DummyBoardItem>> _factoryMock;
        private Mock<DummyBoardItemData> _mockItemData;

        private List<DummyBoardItem> _expectedAnswer;

        [SetUp]
        public void SetUp()
        {
            _itemRepositoryMock = new Mock<IBoardItemsRepository<DummyBoardItem>>();
            _factoryMock = new Mock<IFactory<DummyBoardItemData, Coordinate, DummyBoardItem>>();
            _expectedAnswer = new List<DummyBoardItem>();
            var emptySprite = Sprite.Create(null, Rect.zero, Vector2.zero);
            _mockItemData = new Mock<DummyBoardItemData>(emptySprite, string.Empty);

            _factoryMock
                .Setup(f => f.Create(It.IsAny<DummyBoardItemData>(), It.IsAny<Coordinate>()))
                .Returns(
                    (DummyBoardItemData data, Coordinate coordinate) =>
                    {
                        var dummyBoardItem = new DummyBoardItem(data, coordinate);
                        _expectedAnswer.Add(dummyBoardItem);
                        return dummyBoardItem;
                    }
                );

            _loadService = new LoadBoardItemsService<DummyBoardItem, DummyBoardItemData>(
                new Mock<ICoordinateService>().Object,
                _factoryMock.Object,
                _itemRepositoryMock.Object
            );
        }

        [Test]
        public void WhenLoadWithValidParameter_ItemRepositoryIsSetWithCorrectValues()
        {
            var datas = Enumerable.Repeat(_mockItemData.Object, 9).ToList();
            const int mapWidth = 3;
            const int mapHeight = 3;

            _loadService.Load(datas, mapWidth, mapHeight);

            _itemRepositoryMock.Verify(r => r.Set(It.Is<List<DummyBoardItem>>(actual => actual.SequenceEqual(_expectedAnswer))));
        }

        [Test]
        public void WhenLoadWithEmptyMap_ItemRepositoryIsSetEmptyList()
        {
            var datas = new DummyBoardItemData[0];
            const int mapWidth = 0;
            const int mapHeight = 0;

            _loadService.Load(datas, mapWidth, mapHeight);

            _itemRepositoryMock.Verify(r => r.Set(It.Is<List<DummyBoardItem>>(actual => !actual.Any())));
        }

        [Test]
        public void WhenLoadWithMoreDataThanMapCanHold_ThrowsArgumentException()
        {
            var datas = new DummyBoardItemData[1];
            const int mapWidth = 0;
            const int mapHeight = 0;

            void CallWithMoreDataThanMapCanHold()
            {
                _loadService.Load(datas, mapWidth, mapHeight);
            }
            
            Assert.That(CallWithMoreDataThanMapCanHold, Throws.TypeOf<ArgumentException>());
        }

        [UsedImplicitly]
        internal class DummyBoardItem : BoardItem<DummyBoardItemData>
        {
            public DummyBoardItem(DummyBoardItemData data, Coordinate coordinate) : base(data, coordinate)
            {
            }
        }

        [UsedImplicitly]
        internal class DummyBoardItemData : BoardItemData
        {
            public DummyBoardItemData(Sprite icon, string name) : base(icon, name)
            {
            }
        }
    }
}