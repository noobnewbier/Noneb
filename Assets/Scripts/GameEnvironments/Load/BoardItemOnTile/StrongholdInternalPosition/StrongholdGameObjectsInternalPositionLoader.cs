using System;
using System.Collections.Generic;
using Common.Constants;
using Common.Loaders;
using GameEnvironments.Common.Repositories.CurrentLevelData;
using Strongholds;
using Tiles;
using UniRx;
using UnityEngine;
using UnityEngine.Serialization;
using UnityUtils;

namespace GameEnvironments.Load.BoardItemOnTile.StrongholdInternalPosition
{
    public class StrongholdGameObjectsInternalPositionLoader : MonoBehaviour, ILoader
    {
        [SerializeField] private SetupStrongholdGameObjectsInternalPositionServiceProvider serviceProvider;
        [SerializeField] private TilesTransformProvider tilesTransformProvider;

        [FormerlySerializedAs("levelDataRepositoryProvider")] [SerializeField]
        private CurrentLevelDataRepositoryProvider currentLevelDataRepositoryProvider;

        private ISetupStrongholdGameObjectsInternalPositionService _setupStrongholdGameObjectsInternalPositionService;
        private ICurrentLevelDataRepository _currentLevelDataRepository;
        private IDisposable _disposable;

        [ContextMenu(nameof(LoadAndForget))]
        public void LoadAndForget()
        {
            DisposeDisposables();

            _disposable = GetDataTupleObservable()
                .Subscribe(SetupTilesWithStronghold);
        }

        public IObservable<Unit> LoadObservable()
        {
            return GetDataTupleObservable()
                .Select(
                    datas =>
                    {
                        SetupTilesWithStronghold(datas);
                        return Unit.Default;
                    }
                );
        }

        private void OnEnable()
        {
            _setupStrongholdGameObjectsInternalPositionService = serviceProvider.Provide();
            _currentLevelDataRepository = currentLevelDataRepositoryProvider.Provide();
        }
        
        private IObservable<StrongholdData[]> GetDataTupleObservable()
        {
            return _currentLevelDataRepository.Get()
                .Select(d => d.StrongholdDatas)
                .Take(1);
        }

        private void SetupTilesWithStronghold(IReadOnlyList<StrongholdData> strongholdDatas)
        {
            var tiles = tilesTransformProvider.Provide();

            for (var i = 0; i < strongholdDatas.Count; i++)
            {
                if (strongholdDatas[i] == null)
                {
                    continue;
                }

                SetupIndividualTileWithStronghold(tiles[i]);
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