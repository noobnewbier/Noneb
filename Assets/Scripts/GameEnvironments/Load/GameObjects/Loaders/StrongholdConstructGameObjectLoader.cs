using Common.Providers;
using GameEnvironments.Common.Data;

namespace GameEnvironments.Load.GameObjects.Loaders
{
    public class StrongholdConstructGameObjectLoader : GameObjectLoader
    {
        protected override GameObjectProvider[] GetGameObjectProvidersFromGameEnvironment(GameEnvironment environment)
        {
            return environment.StrongholdConstructGameObjectProviders;
        }
    }
}