using System;
using System.Collections.Generic;
using Noneb.Core.Game.Common.Constants;
using Noneb.Core.Game.Common.Loaders;
using Noneb.Ui.Game.Common.Holders;
using Noneb.Ui.Game.GameEnvironments.BoardItemsHoldersFetchingService;
using Noneb.Ui.Game.GameEnvironments.BoardItemsHoldersFetchingService.Providers;
using Noneb.Ui.Game.Strongholds;
using UniRx;
using UnityEngine;
using UnityEngine.Serialization;
using UnityUtils;

namespace Noneb.Ui.Game.GameEnvironments.Load.CleanUp.StrongholdInternalPosition
{
    [CreateAssetMenu(
        fileName = nameof(StrongholdGameObjectsInternalPositionLoader),
        menuName = ProjectMenuName.Loader + nameof(StrongholdGameObjectsInternalPositionLoader)
    )]
    public class StrongholdGameObjectsInternalPositionLoader : ScriptableObject, ILoader
    {
        [SerializeField] private SetupStrongholdGameObjectsInternalPositionServiceProvider serviceProvider;

        [FormerlySerializedAs("strongholdHoldersFetchingServiceRepositoryProvider")]
        [FormerlySerializedAs("strongholdsFetchingServiceRepositoryProvider")]
        [FormerlySerializedAs("strongholdsHolderRepositoryProvider")]
        [SerializeField]
        private StrongholdHoldersFetchingServiceProvider strongholdHoldersFetchingServiceProvider;

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
            _strongholdsHolderFetchingService = strongholdHoldersFetchingServiceProvider.Provide();
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