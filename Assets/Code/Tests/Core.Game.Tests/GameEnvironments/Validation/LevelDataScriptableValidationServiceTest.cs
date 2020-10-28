using Noneb.Core.Game.GameEnvironments.Data.LevelDatas;
using Noneb.Core.Game.GameEnvironments.Validation;
using Noneb.Core.Game.Maps;
using Noneb.Core.Game.Units;
using NUnit.Framework;
using UnityEngine;
using static Core.Game.Tests.GameEnvironments.Validation.LevelDataTestExtensions;

namespace Core.Game.Tests.GameEnvironments.Validation
{
    [TestFixture]
    public class LevelDataScriptableValidationServiceTest
    {
        [SetUp]
        public void SetUp()
        {
            _service = new LevelDataScriptableValidationService();
            _3X3MapConfig = MapConfig.Create(3, 3);
        }

        private LevelDataScriptableValidationService _service;
        private MapConfig _3X3MapConfig;

        [Test]
        public void WhenAllFieldsAreValid_ReturnIsValid()
        {
            var validLevelData = GetEmptyLevelData(_3X3MapConfig).FillWithTiles().ToScriptable();

            var returnedValue = _service.Validate(validLevelData, _3X3MapConfig);

            Assert.That(returnedValue.IsValid, Is.True);
        }

        [Test]
        public void WhenNotAllDataOfCorrectSize_ReturnIsNotValid()
        {
            var emptyLevelData = LevelDataScriptable.Create(LevelData.Empty);

            var returnedValue = _service.Validate(emptyLevelData, _3X3MapConfig);

            Assert.That(returnedValue.IsValid, Is.False);
        }

        [Test]
        public void WhenNotAllTileHasData_ReturnIsNotValid()
        {
            var levelData = GetEmptyLevelData(_3X3MapConfig).FillWithTiles().ToScriptable();
            levelData.TileDatas[0] = null;

            var returnedValue = _service.Validate(levelData, _3X3MapConfig);

            Assert.That(returnedValue.IsValid, Is.False);
        }

        [Test]
        public void WhenStrongholdDataWrapperIsSetUpWithUnitButNotConstruct_ReturnIsNotValid()
        {
            var levelData = GetEmptyLevelData(_3X3MapConfig).FillWithTiles().FillWithStrongholds().ToScriptable();
            levelData.StrongholdDatas[0] = new LevelDataScriptable.StrongholdDataWrapper(ScriptableObject.CreateInstance<UnitDataScriptable>(), null);

            var returnedValue = _service.Validate(levelData, _3X3MapConfig);

            Assert.That(returnedValue.IsValid, Is.False);
        }

        [Test]
        public void WhenStrongholdHasDataButNotConstructGameObjectFactory_ReturnIsNotValid()
        {
            var levelData = GetEmptyLevelData(_3X3MapConfig).FillWithTiles().FillWithStrongholds().ToScriptable();
            levelData.StrongholdConstructGameObjectFactories[0] = null;

            var returnedValue = _service.Validate(levelData, _3X3MapConfig);

            Assert.That(returnedValue.IsValid, Is.False);
        }

        [Test]
        public void WhenStrongholdHasDataButNotUnitGameObjectFactory_ReturnIsNotValid()
        {
            var levelData = GetEmptyLevelData(_3X3MapConfig).FillWithTiles().FillWithStrongholds().ToScriptable();
            levelData.StrongholdUnitGameObjectFactories[0] = null;

            var returnedValue = _service.Validate(levelData, _3X3MapConfig);

            Assert.That(returnedValue.IsValid, Is.False);
        }

        [Test]
        public void WhenThereIsBothStrongholdAndConstructInSameCoordinate_ReturnIsNotValid()
        {
            var levelData = GetEmptyLevelData(_3X3MapConfig).FillWithTiles().FillWithConstructs().FillWithStrongholds().ToScriptable();

            var returnedValue = _service.Validate(levelData, _3X3MapConfig);

            Assert.That(returnedValue.IsValid, Is.False);
        }

        [Test]
        public void WhenThereIsBothStrongholdAndUnitInSameCoordinate_ReturnIsNotValid()
        {
            var levelData = GetEmptyLevelData(_3X3MapConfig).FillWithTiles().FillWithUnits().FillWithStrongholds().ToScriptable();

            var returnedValue = _service.Validate(levelData, _3X3MapConfig);

            Assert.That(returnedValue.IsValid, Is.False);
        }

        [Test]
        public void WhenUnitHasDataButNotGameObjectFactory_ReturnIsNotValid()
        {
            var levelData = GetEmptyLevelData(_3X3MapConfig).FillWithTiles().FillWithUnits().ToScriptable();
            levelData.UnitGameObjectFactories[0] = null;

            var returnedValue = _service.Validate(levelData, _3X3MapConfig);

            Assert.That(returnedValue.IsValid, Is.False);
        }

        [Test]
        public void WhenUnitHasGameObjectFactoryButNotData_ReturnIsNotValid()
        {
            var levelData = GetEmptyLevelData(_3X3MapConfig).FillWithTiles().FillWithUnits().ToScriptable();
            levelData.UnitDatas[0] = null;

            var returnedValue = _service.Validate(levelData, _3X3MapConfig);

            Assert.That(returnedValue.IsValid, Is.False);
        }
    }
}