using Main.Ui.Game.GameEnvironments.BoardItemsFetcherRepository.Providers;
using Main.Ui.Game.Tiles;
using UnityEngine;
using UnityEngine.Serialization;

namespace Main.Ui.Game.GameEnvironments.BoardItemsFetcherRepository.Setters
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