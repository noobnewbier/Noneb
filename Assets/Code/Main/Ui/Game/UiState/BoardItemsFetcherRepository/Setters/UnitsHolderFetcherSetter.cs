using Main.Ui.Game.UiState.BoardItemsFetcherRepository.Providers;
using Main.Ui.Game.Units;
using UnityEngine;
using UnityEngine.Serialization;

namespace Main.Ui.Game.UiState.BoardItemsFetcherRepository.Setters
{
    public class UnitsHolderFetcherSetter : MonoBehaviour
    {
        [SerializeField] private UnitsHolderFetcherRepositoryProvider repositoryProvider;

        [FormerlySerializedAs("holdersProvider")] [SerializeField]
        private UnitsHolderFetcher holdersFetcher;

        [ContextMenu(nameof(Set))]
        public void Set()
        {
            repositoryProvider.Provide().Set(holdersFetcher);
        }
    }
}