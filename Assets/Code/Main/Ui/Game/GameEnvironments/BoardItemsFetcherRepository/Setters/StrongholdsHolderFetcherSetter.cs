using Main.Ui.Game.GameEnvironments.BoardItemsFetcherRepository.Providers;
using Main.Ui.Game.Strongholds;
using UnityEngine;
using UnityEngine.Serialization;

namespace Main.Ui.Game.GameEnvironments.BoardItemsFetcherRepository.Setters
{
    public class StrongholdsHolderFetcherSetter : MonoBehaviour
    {
        [SerializeField] private StrongholdsHolderFetcherRepositoryProvider repositoryProvider;

        [FormerlySerializedAs("holdersProvider")] [SerializeField]
        private StrongholdsHolderFetcher holdersFetcher;

        [ContextMenu(nameof(Set))]
        public void Set()
        {
            repositoryProvider.Provide().Set(holdersFetcher);
        }
    }
}