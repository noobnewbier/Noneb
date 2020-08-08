using Common.Constants;
using GameEnvironments.Common.Repositories.CurrentGameEnvironment;
using GameEnvironments.Common.Repositories.LevelDatas;
using Tiles;
using UnityEngine;
using UnityUtils;

namespace GameEnvironments.Load.BoardItemOnTile.StrongholdInternalPosition
{
    public class StrongholdGameObjectsInternalPositionSetupper : MonoBehaviour
    {
        [SerializeField] private SetupStrongholdGameObjectsInternalPositionServiceProvider serviceProvider;
        [SerializeField] private TilesTransformProvider tilesTransformProvider;
        [SerializeField] private LevelDataRepositoryProvider levelDataRepositoryProvider;
        
        private ISetupStrongholdGameObjectsInternalPositionService _setupStrongholdGameObjectsInternalPositionService;
        private ILevelDataRepository _levelDataRepository;

        private void OnEnable()
        {
            _setupStrongholdGameObjectsInternalPositionService = serviceProvider.Provide();
            _levelDataRepository = levelDataRepositoryProvider.Provide();
        }

        [ContextMenu(nameof(Setup))]
        public void Setup()
        {
            var tiles = tilesTransformProvider.Provide();
            var strongholdDatas = _levelDataRepository.StrongholdDatas;

            for (var i = 0; i < strongholdDatas.Length; i++)
            {
                if (strongholdDatas[i] == null)
                {
                    continue;
                }

                SetupTileWithStronghold(tiles[i]);
            }
        }

        private void SetupTileWithStronghold(Transform tileTransform)
        {
            var unitGameObject = tileTransform.FindChildWithTag(ObjectTags.UnitGameObject).gameObject;
            var constructGameObject = tileTransform.FindChildWithTag(ObjectTags.ConstructGameObject).gameObject;

            _setupStrongholdGameObjectsInternalPositionService.SetPosition(
                unitGameObject,
                constructGameObject
            );
        }
    }
}