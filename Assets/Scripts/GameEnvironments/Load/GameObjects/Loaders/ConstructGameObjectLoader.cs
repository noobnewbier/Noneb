using Common.Providers;
using GameEnvironments.Common.Data;

namespace GameEnvironments.Load.GameObjects.Loaders
{
    public class ConstructGameObjectLoader : GameObjectLoader
    {
        protected override GameObjectProvider[] GetGameObjectProvidersFromGameEnvironment(GameEnvironment environment)
        {
            return environment.ConstructGameObjectProviders;
        }
    }
}