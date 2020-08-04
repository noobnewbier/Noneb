using System.Linq;
using Common.BoardItems;
using Common.Providers;
using Tiles;
using UnityEngine;

namespace Common.Holders
{
    public abstract class HoldersInitializer : MonoBehaviour
    {
        public abstract void InitializeHolder();
    }

    /// <summary>
    /// This initializer strongly depends the structure which look like this:
    ///     1. TileTransform
    ///         a.Mono(holds TR and preservation of T)
    ///
    /// Where it can only handle ONE such instance, and ignore any TR under the same parent.
    /// This is built under the expectation of "There can be no more than ONE thing of some type under one tile"
    /// We will see if this bit us later :)
    ///
    /// Todo: may need to get rid of this
    /// </summary>
    public abstract class HoldersInitializer<T, THolder> : HoldersInitializer where T : BoardItem where THolder : IBoardItemHolder<T>
    {
        [SerializeField] private TilesTransformProvider tilesTransformProvider;
        protected abstract string HolderTag { get; }

        public sealed override void InitializeHolder()
        {
            var tileHolders = tilesTransformProvider.Provide();

            foreach (var transformHasTr in tileHolders
                .Select(holder => holder.transform)
                .Select(GetTrTransform)
                .Where(t => t != null))
                InitializeHolder(transformHasTr);
        }

        private Transform GetTrTransform(Transform tileTransform)
        {
            return tileTransform.CompareTag(HolderTag) ?
                tileTransform :
                tileTransform.Cast<Transform>().FirstOrDefault(t => t.CompareTag(HolderTag));
        }

        private static void InitializeHolder(Component representationTransform)
        {
            var preservationContainer = representationTransform.GetComponent<IPreservationContainer<T>>();
            var preservedT = preservationContainer.GetPreservation();
            representationTransform.GetComponent<THolder>().Initialize(preservedT);
        }
    }
}