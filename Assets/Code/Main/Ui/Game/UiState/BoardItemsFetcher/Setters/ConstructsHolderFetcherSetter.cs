using Main.Ui.Game.Constructs;
using Main.Ui.Game.UiState.BoardItemsFetcher.Providers;
using UnityEngine;
using UnityEngine.Serialization;

namespace Main.Ui.Game.UiState.BoardItemsFetcher.Setters
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