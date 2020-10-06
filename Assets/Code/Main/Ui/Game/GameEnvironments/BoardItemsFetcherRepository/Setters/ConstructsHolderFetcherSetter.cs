using Main.Ui.Game.Constructs;
using Main.Ui.Game.GameEnvironments.BoardItemsFetcherRepository.Providers;
using UnityEngine;
using UnityEngine.Serialization;

namespace Main.Ui.Game.GameEnvironments.BoardItemsFetcherRepository.Setters
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