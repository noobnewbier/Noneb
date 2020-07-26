using System.Linq;
using Common.Providers;
using Tiles;
using UnityEngine;

namespace Common.Holders
{
    public abstract class HoldersInitializer : MonoBehaviour
    {
        public abstract void InitializeRepresentation();
    }

    /// <summary>
    /// This initializer strongly depends the structure which look like this:
    ///     1. TileTransform
    ///         a.Mono(holds TR and preservation of T)
    ///
    /// Where it can only handle ONE such instance, and ignore any TR under the same parent.
    /// This is built under the expectation of "There can be no more than ONE thing of some type under one tile"
    /// We will see if this bit us later :)
    /// </summary>
    public abstract class HoldersInitializer<T, TR> : HoldersInitializer where T : class where TR : IHolder<T>
    {
        [SerializeField] private TilesTransformProvider tilesTransformProvider;
        protected abstract string RepresentationTag { get; }

        public sealed override void InitializeRepresentation()
        {
            var tileRepresentations = tilesTransformProvider.Provide();

            foreach (var transformHasTr in tileRepresentations
                .Select(tileRepresentation => tileRepresentation.transform)
                .Select(GetTrTransform)
                .Where(t => t != null))
                InitializeRepresentation(transformHasTr);
        }

        private Transform GetTrTransform(Transform tileTransform)
        {
            return tileTransform.CompareTag(RepresentationTag) ?
                tileTransform :
                tileTransform.Cast<Transform>().FirstOrDefault(t => t.CompareTag(RepresentationTag));
        }

        private static void InitializeRepresentation(Component representationTransform)
        {
            var preservationContainer = representationTransform.GetComponent<IPreservationContainer<T>>();
            var preservedT = preservationContainer.GetPreservation();
            representationTransform.GetComponent<TR>().Initialize(preservedT);
        }
    }
}