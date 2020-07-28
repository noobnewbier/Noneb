using Common.Holders;
using UnityEngine;
using UnityEngine.Serialization;

namespace DebugUtils
{
    public class InitializersInvoker : MonoBehaviour
    {
        [FormerlySerializedAs("representationInitializers")] [SerializeField] private HoldersInitializer[] holdersInitializers;

        [ContextMenu("Initialize")]
        public void Initialize()
        {
            foreach (var representationInitializer in holdersInitializers) representationInitializer.InitializeHolder();
        }
    }
}