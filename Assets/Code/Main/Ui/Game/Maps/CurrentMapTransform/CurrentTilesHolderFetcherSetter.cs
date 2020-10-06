using Main.Ui.Game.GameEnvironments.BoardItemsFetcherRepository.Providers;
using Main.Ui.Game.Tiles;
using UnityEngine;
using UnityEngine.Serialization;

namespace Main.Ui.Game.Maps.CurrentMapTransform
{
    public class CurrentTilesHolderFetcherSetter : MonoBehaviour
    {
        [FormerlySerializedAs("currentTilesTransformRepositoryProvider")] [SerializeField]
        private TilesHolderFetcherRepositoryProvider repositoryProvider;

        [FormerlySerializedAs("tilesHolderProvider")] [FormerlySerializedAs("tilesTransformProvider")] [SerializeField]
        private TilesHolderFetcher tilesHolderFetcher;

        [ContextMenu(nameof(Set))]
        public void Set()
        {
            repositoryProvider.Provide().Set(tilesHolderFetcher);
        }
    }
}