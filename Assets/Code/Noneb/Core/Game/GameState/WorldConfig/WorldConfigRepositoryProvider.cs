using Noneb.Core.Game.Common.Providers;
using Noneb.Core.Game.GameState.GameEnvironments;
using UnityEngine;
using UnityEngine.Serialization;
using UnityUtils.Constants;

namespace Noneb.Core.Game.GameState.WorldConfig
{
    [CreateAssetMenu(
        fileName = nameof(WorldConfigRepositoryProvider),
        menuName = MenuName.ScriptableRepository + nameof(WorldConfigRepository)
    )]
    public class WorldConfigRepositoryProvider : ScriptableObject, IObjectProvider<IWorldConfigRepository>
    {
        [FormerlySerializedAs("currentGameEnvironmentRepositoryProvider")] [SerializeField] private GameEnvironmentRepositoryProvider selectedGameEnvironmentRepositoryProvider;

        private IWorldConfigRepository _cache;

        public IWorldConfigRepository Provide() =>
            _cache ?? (_cache = new WorldConfigRepository(selectedGameEnvironmentRepositoryProvider.Provide()));
    }
}