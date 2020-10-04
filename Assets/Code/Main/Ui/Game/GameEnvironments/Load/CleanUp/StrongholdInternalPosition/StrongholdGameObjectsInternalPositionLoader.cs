using System;
using System.Collections.Generic;
using Common;
using Common.Constants;
using Common.Holders;
using Common.Loaders;
using GameEnvironments.Common.Repositories.BoardItemsHolders;
using GameEnvironments.Common.Repositories.BoardItemsHolders.Providers;
using Strongholds;
using UniRx;
using UnityEngine;
using UnityEngine.Serialization;
using UnityUtils;

namespace GameEnvironments.Load.CleanUp.StrongholdInternalPosition
{
    [CreateAssetMenu(
        fileName = nameof(StrongholdGameObjectsInternalPositionLoader),
        menuName = ProjectMenuName.Loader + nameof(StrongholdGameObjectsInternalPositionLoader)
    )]
    public class StrongholdGameObjectsInternalPositionLoader : ScriptableObject, ILoader
    {
        [SerializeField] private SetupStrongholdGameObjectsInternalPositionServiceProvider serviceProvider;

        [FormerlySerializedAs("strongholdsFetchingServiceRepositoryProvider")]
        [FormerlySerializedAs("strongholdsHolderRepositoryProvider")]
        [SerializeField]
        private StrongholdHoldersFetchingServiceRepositoryProvider strongholdHoldersFetchingServiceRepositoryProvider;

        private ISetupStrongholdGameObjectsInternalPositionService _setupStrongholdGameObjectsInternalPositionService;
        private IBoardItemHoldersFetchingService<StrongholdHolder> _strongholdsHolderFetchingService;
        private IDisposable _disposable;

        [ContextMenu(nameof(LoadAndForget))]
        public void LoadAndForget()
        {
            DisposeDisposables();

            _disposable = _strongholdsHolderFetchingService.Fetch()
                .Subscribe(SetupTilesWithStronghold);
        }

        public IObservable<Unit> LoadObservable()
        {
            return _strongholdsHolderFetchingService.Fetch()
                .Select(
                    holders =>
                    {
                        SetupTilesWithStronghold(holders);

                        return Unit.Default;
                    }
                );
        }

        private void OnEnable()
        {
            _setupStrongholdGameObjectsInternalPositionService = serviceProvider.Provide();
            _strongholdsHolderFetchingService = strongholdHoldersFetchingServiceRepositoryProvider.Provide();
        }

        private void SetupTilesWithStronghold(IReadOnlyList<StrongholdHolder> strongholdHolders)
        {
            foreach (var strongholdHolder in strongholdHolders)
            {
                if (strongholdHolder == null)
                {
                    continue;
                }

                SetupIndividualTileWithStronghold(strongholdHolder);
            }
        }

        private void SetupIndividualTileWithStronghold(IBoardItemHolder strongholdHolder)
        {
            var strongholdTransform = strongholdHolder.Transform;
            var unitGameObject = strongholdTransform.FindChildWithTag(ObjectTags.UnitGameObject).gameObject;
            var constructGameObject = strongholdTransform.FindChildWithTag(ObjectTags.ConstructGameObject).gameObject;

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