using Common;
using UnityEngine;

namespace Constructs
{
    [CreateAssetMenu(menuName = "Providers/ConstructRepresentation", fileName = "ConstructRepresentationProvider")]
    public class ConstructRepresentationProvider : ScriptableObjectProvider<ConstructRepresentation>
    {
        [SerializeField] private GameObject prefab;
        
        public override ConstructRepresentation Provide()
        {
            return Instantiate(prefab).GetComponent<ConstructRepresentation>();
        }
    }
}