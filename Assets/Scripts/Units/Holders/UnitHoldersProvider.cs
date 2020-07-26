using Common.Providers;
using UnityEngine;

namespace Units.Holders
{
    [CreateAssetMenu(menuName = "Providers/UnitRepresentation", fileName = nameof(UnitHoldersProvider))]
    public class UnitHoldersProvider : ScriptableObjectProvider<UnitHolder>
    {
        [SerializeField] private GameObject prefab;
        
        public override UnitHolder Provide()
        {
            var clone = Instantiate(prefab);

            return clone.GetComponent<UnitHolder>();
        }
    }
}