using Common;
using UnityEngine;

namespace Units.Representation
{
    [CreateAssetMenu(menuName = "Providers/UnitRepresentation", fileName = "UnitRepresentationProvider")]
    public class UnitRepresentationProvider : ScriptableObjectProvider<UnitRepresentation>
    {
        [SerializeField] private GameObject prefab;
        
        public override UnitRepresentation Provide()
        {
            var clone = Instantiate(prefab);

            return clone.GetComponent<UnitRepresentation>();
        }
    }
}