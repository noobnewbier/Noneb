using Common.Providers;
using Constructs;
using GameEnvironments.Common.Data;
using Tiles.Data;
using Units;

namespace GameEnvironments.Common.Repositories.CurrentGameEnvironment
{
    public class CurrentGameEnvironmentRepositoryProvider : MonoObjectProvider<ICurrentGameEnvironmentRepository>
    {
        public override ICurrentGameEnvironmentRepository Provide()
        {
            //todo: actual game env
            var gameEnv = new GameEnvironment(
                new GameObjectProvider[1],
                new ConstructData[0],
                new GameObjectProvider[0],
                new UnitData[0],
                new GameObjectProvider[0],
                new TileData[0],
                null
            );
            return new CurrentGameEnvironmentRepository(gameEnv);
        }
    }
}