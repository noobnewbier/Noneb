using Main.Ui.Game.Tiles;
using Main.Ui.Game.UiState.BoardItemsFetcher.Providers;
using UnityEngine;
using UnityEngine.Serialization;

namespace Main.Ui.Game.UiState.BoardItemsFetcher.Setters
{
    public class TilesHolderFetcherSetter : MonoBehaviour
    {
        [SerializeField] private TilesHolderFetcherRepositoryProvider repositoryProvider;

        [FormerlySerializedAs("holdersProvider")] [SerializeField]
        private TilesHolderFetcher holdersFetcher;

        [ContextMenu(nameof(Set))]
        public void Set()
        {
            repositoryProvider.Provide().Set(holdersFetcher);
        }
    }
}