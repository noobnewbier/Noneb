using Common;
using UnityEngine;

namespace Construct
{
    [CreateAssetMenu(menuName = "Providers/ConstructRepresentation", fileName = "ConstructRepresentationProvider")]
    public class ConstructRepresentationProvider : ObjectProvider<ConstructRepresentation>
    {
        [SerializeField] private GameObject prefab;
        
        public override ConstructRepresentation Provide()
        {
            return Instantiate(prefab).GetComponent<ConstructRepresentation>();
        }
    }
}