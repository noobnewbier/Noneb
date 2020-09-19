using System;
using System.Collections.Generic;
using Common;
using Common.Constants;
using Common.Loaders;
using GameEnvironments.Common.Repositories.CurrentLevelData;
using Maps.Repositories.CurrentTilesTransform;
using Strongholds;
using UniRx;
using UnityEngine;
using UnityUtils;

namespace GameEnvironments.Load.Obsolete.BoardItemOnTile.StrongholdInternalPosition
{
    [CreateAssetMenu(
        fileName = nameof(StrongholdGameObjectsInternalPositionLoader),
        menuName = ProjectMenuName.Loader + nameof(StrongholdGameObjectsInternalPositionLoader)
    )]
    public class StrongholdGameObjectsInternalPositionLoader : ScriptableObject, ILoader
    {
        [SerializeField] private SetupStrongholdGameObjectsInternalPositionServiceProvider serviceProvider;
        [SerializeField] private CurrentTilesTransformRepositoryProvider currentTilesTransformRepositoryProvider;
        [SerializeField] private CurrentLevelDataRepositoryProvider currentLevelDataRepositoryProvider;

        private ISetupStrongholdGameObjectsInternalPositionService _setupStrongholdGameObjectsInternalPositionService;
        private ICurrentTilesTransformGetRepository _currentTilesTransformGetRepository;
        private ICurrentLevelDataRepository _currentLevelDataRepository;
        private IDisposable _disposable;

        [ContextMenu(nameof(LoadAndForget))]
        public void LoadAndForget()
        {
            DisposeDisposables();

            _disposable = GetDataTupleObservable()
                .ZipLatest(_currentTilesTransformGetRepository.GetMostRecent(), (datas, tilesTransform) => (datas, tilesTransform))
                .Subscribe(
                    tuple =>
                    {
                        var (datas, tilesTransform) = tuple;
                        SetupTilesWithStronghold(datas, tilesTransform);
                    }
                );
        }

        public IObservable<Unit> LoadObservable()
        {
            return GetDataTupleObservable()
                .ZipLatest(_currentTilesTransformGetRepository.GetMostRecent(), (datas, tilesTransform) => (datas, tilesTransform))
                .Select(
                    tuple =>
                    {
                        var (datas, tilesTransform) = tuple;
                        SetupTilesWithStronghold(datas, tilesTransform);

                        return Unit.Default;
                    }
                );
        }

        private void OnEnable()
        {
            _setupStrongholdGameObjectsInternalPositionService = serviceProvider.Provide();
            _currentLevelDataRepository = currentLevelDataRepositoryProvider.Provide();
            _currentTilesTransformGetRepository = currentTilesTransformRepositoryProvider.Provide();
        }

        private IObservable<StrongholdData[]> GetDataTupleObservable()
        {
            return _currentLevelDataRepository.GetMostRecent()
                .Select(d => d.StrongholdDatas);
        }

        private void SetupTilesWithStronghold(IReadOnlyList<StrongholdData> strongholdDatas, IList<Transform> tilesTransform)
        {
            for (var i = 0; i < strongholdDatas.Count; i++)
            {
                if (strongholdDatas[i] == null)
                {
                    continue;
                }

                SetupIndividualTileWithStronghold(tilesTransform[i]);
            }
        }

        private void SetupIndividualTileWithStronghold(Transform tileTransform)
        {
            var unitGameObject = tileTransform.FindChildWithTag(ObjectTags.UnitGameObject).gameObject;
            var constructGameObject = tileTransform.FindChildWithTag(ObjectTags.ConstructGameObject).gameObject;

            _setupStrongholdGameObjectsInternalPositionService.SetPosition(
                unitGameObject,
                constructGameObject
            );
        }

        private void OnDisable()
        {
            DisposeDisposables();
        }

        private void DisposeDisposables()
        {
            _disposable?.Dispose();
        }
    }
}