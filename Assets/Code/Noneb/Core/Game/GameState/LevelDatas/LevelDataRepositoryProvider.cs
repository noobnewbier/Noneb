using Noneb.Core.Game.Common.Providers;
using Noneb.Core.Game.GameState.GameEnvironments;
using UnityEngine;
using UnityEngine.Serialization;
using UnityUtils.Constants;

namespace Noneb.Core.Game.GameState.LevelDatas
{
    [CreateAssetMenu(
        fileName = nameof(LevelDataRepositoryProvider),
        menuName = MenuName.ScriptableRepository + nameof(LevelDataRepository)
    )]
    public class LevelDataRepositoryProvider : ScriptableObject, IObjectProvider<ILevelDataRepository>
    {
        [FormerlySerializedAs("currentGameEnvironmentRepositoryProvider")] [SerializeField] private GameEnvironmentRepositoryProvider selectedGameEnvironmentRepositoryProvider;

        private ILevelDataRepository _cache;

        public ILevelDataRepository Provide() =>
            _cache ??
            (_cache = new LevelDataRepository(
                selectedGameEnvironmentRepositoryProvider.Provide()
            ));
    }
}