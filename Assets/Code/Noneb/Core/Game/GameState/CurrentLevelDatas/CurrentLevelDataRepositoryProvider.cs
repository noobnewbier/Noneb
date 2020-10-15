using Noneb.Core.Game.Common.Providers;
using Noneb.Core.Game.GameState.CurrentGameEnvironments;
using UnityEngine;
using UnityUtils.Constants;

namespace Noneb.Core.Game.GameState.CurrentLevelDatas
{
    [CreateAssetMenu(
        fileName = nameof(CurrentLevelDataRepositoryProvider),
        menuName = MenuName.ScriptableRepository + nameof(CurrentLevelDataRepository)
    )]
    public class CurrentLevelDataRepositoryProvider : ScriptableObject, IObjectProvider<ICurrentLevelDataRepository>
    {
        [SerializeField] private CurrentGameEnvironmentRepositoryProvider currentGameEnvironmentRepositoryProvider;

        private ICurrentLevelDataRepository _cache;

        public ICurrentLevelDataRepository Provide() =>
            _cache ??
            (_cache = new CurrentLevelDataRepository(
                currentGameEnvironmentRepositoryProvider.Provide()
            ));
    }
}