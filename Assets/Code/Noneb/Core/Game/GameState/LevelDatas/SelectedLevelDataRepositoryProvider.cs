using Noneb.Core.Game.Common.Providers;
using Noneb.Core.Game.GameState.GameEnvironments;
using UnityEngine;
using UnityEngine.Serialization;
using UnityUtils.Constants;

namespace Noneb.Core.Game.GameState.LevelDatas
{
    [CreateAssetMenu(
        fileName = nameof(SelectedLevelDataRepositoryProvider),
        menuName = MenuName.ScriptableRepository + nameof(LevelDataRepository)
    )]
    public class SelectedLevelDataRepositoryProvider : ScriptableObject, IObjectProvider<ILevelDataRepository>
    {
        [FormerlySerializedAs("currentGameEnvironmentRepositoryProvider")] [SerializeField] private SelectedGameEnvironmentRepositoryProvider selectedGameEnvironmentRepositoryProvider;

        private ILevelDataRepository _cache;

        public ILevelDataRepository Provide() =>
            _cache ??
            (_cache = new LevelDataRepository(
                selectedGameEnvironmentRepositoryProvider.Provide()
            ));
    }
}