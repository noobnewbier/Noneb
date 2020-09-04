using System;
using GameEnvironments.Common.Data;
using GameEnvironments.Common.Repositories.CurrentGameEnvironment;
using UniRx;
using UnityEngine;

namespace GameEnvironments.Load.Manager
{
    /// <summary>
    /// We clear the scene by loading empty data
    /// </summary>
    public class GameEnvironmentClearer : MonoBehaviour
    {
        [SerializeField] private GameEnvironmentLoader gameEnvironmentLoader;
        [SerializeField] private CurrentGameEnvironmentRepositoryProvider currentGameEnvironmentRepositoryProvider;

        private IDisposable _disposable;
        private ICurrentGameEnvironmentSetRepository _gameEnvironmentSetRepository;

        private void OnEnable()
        {
            _gameEnvironmentSetRepository = currentGameEnvironmentRepositoryProvider.Provide();
        }

        [ContextMenu(nameof(Clear))]
        public void Clear()
        {
            _disposable?.Dispose();

            _gameEnvironmentSetRepository.Set(GameEnvironment.Empty);

            _disposable = gameEnvironmentLoader.GetLoadObservable()
                .Subscribe(
                    _ =>
                    {
                        //todo: proper error handling
                        Debug.Log("level cleared");
                    }
                );
        }

        private void OnDisable()
        {
            _disposable?.Dispose();
        }
    }
}