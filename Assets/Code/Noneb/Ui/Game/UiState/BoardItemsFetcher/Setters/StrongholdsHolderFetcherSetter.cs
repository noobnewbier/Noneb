using Noneb.Ui.Game.Strongholds;
using Noneb.Ui.Game.UiState.BoardItemsFetcher.Providers;
using UnityEngine;
using UnityEngine.Serialization;

namespace Noneb.Ui.Game.UiState.BoardItemsFetcher.Setters
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