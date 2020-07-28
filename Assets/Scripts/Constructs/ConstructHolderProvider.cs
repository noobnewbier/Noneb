using Common;
using Common.Providers;
using UnityEngine;

namespace Constructs
{
    [CreateAssetMenu(menuName = "Providers/ConstructHolder", fileName = nameof(ConstructHolderProvider))]
    public class ConstructHolderProvider : ScriptableObjectProvider<ConstructHolder>
    {
        [SerializeField] private GameObject prefab;
        
        public override ConstructHolder Provide()
        {
            return Instantiate(prefab).GetComponent<ConstructHolder>();
        }
    }
}