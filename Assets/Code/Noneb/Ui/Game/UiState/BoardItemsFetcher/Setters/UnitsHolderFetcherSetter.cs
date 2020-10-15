using Noneb.Ui.Game.UiState.BoardItemsFetcher.Providers;
using Noneb.Ui.Game.Units;
using UnityEngine;
using UnityEngine.Serialization;

namespace Noneb.Ui.Game.UiState.BoardItemsFetcher.Setters
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