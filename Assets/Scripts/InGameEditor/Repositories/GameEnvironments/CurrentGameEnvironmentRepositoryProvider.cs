using Common.Providers;
using Constructs;
using InGameEditor.Data;
using Tiles.Data;
using Tiles.Representation;
using Units;
using Units.Representation;

namespace InGameEditor.Repositories.GameEnvironments
{
    public class CurrentGameEnvironmentRepositoryProvider : MonoObjectProvider<ICurrentGameEnvironmentRepository>
    {
        public override ICurrentGameEnvironmentRepository Provide()
        {
            //todo: actual game env
            var gameEnv = new GameEnvironment(
                new TileRepresentationProvider[1],
                new ConstructData[0],
                new ConstructRepresentationProvider[0],
                new UnitData[0],
                new UnitRepresentationProvider[0],
                new TileData[0],
                null
            );
            return new CurrentGameEnvironmentRepository(gameEnv);
        }
    }
}