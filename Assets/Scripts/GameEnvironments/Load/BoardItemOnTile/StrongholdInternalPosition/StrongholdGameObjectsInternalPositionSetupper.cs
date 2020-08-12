using Common.Constants;
using GameEnvironments.Common.Repositories.CurrentLevelData;
using Tiles;
using UnityEngine;
using UnityEngine.Serialization;
using UnityUtils;

namespace GameEnvironments.Load.BoardItemOnTile.StrongholdInternalPosition
{
    public class StrongholdGameObjectsInternalPositionSetupper : MonoBehaviour
    {
        [SerializeField] private SetupStrongholdGameObjectsInternalPositionServiceProvider serviceProvider;
        [SerializeField] private TilesTransformProvider tilesTransformProvider;

        [FormerlySerializedAs("levelDataRepositoryProvider")] [SerializeField]
        private CurrentLevelDataRepositoryProvider currentLevelDataRepositoryProvider;

        private ISetupStrongholdGameObjectsInternalPositionService _setupStrongholdGameObjectsInternalPositionService;
        private ICurrentLevelDataRepository _currentLevelDataRepository;

        private void OnEnable()
        {
            _setupStrongholdGameObjectsInternalPositionService = serviceProvider.Provide();
            _currentLevelDataRepository = currentLevelDataRepositoryProvider.Provide();
        }

        [ContextMenu(nameof(Setup))]
        public void Setup()
        {
            var tiles = tilesTransformProvider.Provide();
            var strongholdDatas = _currentLevelDataRepository.StrongholdDatas;

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