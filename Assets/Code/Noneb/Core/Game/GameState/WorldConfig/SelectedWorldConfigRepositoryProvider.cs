using Noneb.Core.Game.Common.Providers;
using Noneb.Core.Game.GameState.CurrentGameEnvironments;
using UnityEngine;
using UnityEngine.Serialization;
using UnityUtils.Constants;

namespace Noneb.Core.Game.GameState.CurrentWorldConfig
{
    [CreateAssetMenu(
        fileName = nameof(SelectedWorldConfigRepositoryProvider),
        menuName = MenuName.ScriptableRepository + nameof(WorldConfigRepository)
    )]
    public class SelectedWorldConfigRepositoryProvider : ScriptableObject, IObjectProvider<IWorldConfigRepository>
    {
        [FormerlySerializedAs("currentGameEnvironmentRepositoryProvider")] [SerializeField] private SelectedGameEnvironmentRepositoryProvider selectedGameEnvironmentRepositoryProvider;

        private IWorldConfigRepository _cache;

        public IWorldConfigRepository Provide() =>
            _cache ?? (_cache = new WorldConfigRepository(selectedGameEnvironmentRepositoryProvider.Provide()));
    }
}