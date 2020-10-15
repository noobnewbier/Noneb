using Noneb.Ui.Game.Constructs;
using Noneb.Ui.Game.UiState.BoardItemsFetcher.Providers;
using UnityEngine;
using UnityEngine.Serialization;

namespace Noneb.Ui.Game.UiState.BoardItemsFetcher.Setters
{
    public class ConstructsHolderFetcherSetter : MonoBehaviour
    {
        [SerializeField] private ConstructsHolderFetcherRepositoryProvider repositoryProvider;

        [FormerlySerializedAs("holdersProvider")] [SerializeField]
        private ConstructsHolderFetcher holdersFetcher;

        [ContextMenu(nameof(Set))]
        public void Set()
        {
            repositoryProvider.Provide().Set(holdersFetcher);
        }
    }
}