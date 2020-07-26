using Common.Holders;
using UnityEngine;

namespace DebugUtils
{
    public class InitializersInvoker : MonoBehaviour
    {
        [SerializeField] private HoldersInitializer[] representationInitializers;

        [ContextMenu("Initialize")]
        public void Initialize()
        {
            foreach (var representationInitializer in representationInitializers) representationInitializer.InitializeRepresentation();
        }
    }
}