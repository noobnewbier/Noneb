using System.Collections;
using System.Linq;
using Common;
using Tiles.Representation.Repository;
using Units.Representation;
using UnityEngine;

namespace Units
{
    public class UnitInitializer : MonoBehaviour
    {
        [SerializeField] private TileRepresentationRepositoryProvider tileRepresentationRepositoryProvider;

        [ContextMenu("InitializeUnits")]
        private void InitializeUnits()
        {
            var repository = tileRepresentationRepositoryProvider.Provide();
            var tileRepresentations = repository.GetAllFlatten().ToList();

            foreach (var tileTransform in tileRepresentations
                .Select(tileRepresentation => tileRepresentation.transform)
                .Where(TransformHasUnit))
                InitializeUnit(tileTransform);
        }

        private static bool TransformHasUnit(IEnumerable tileTransform)
        {
            return tileTransform.Cast<Transform>().Any(child => child.CompareTag(ObjectTags.UnitRepresentation));
        }


        private static void InitializeUnit(Transform tileTransform)
        {
            //only one unit is allowed per tile
            for (var i = 0; i < tileTransform.childCount; i++)
            {
                var unitTransform = tileTransform.GetChild(i);
                var container = unitTransform.GetComponent<IPreservationContainer<Unit>>();
                var unit = container.GetPreservation();
                unitTransform.GetComponent<UnitRepresentation>().Initialize(unit);
            }
        }
    }
}