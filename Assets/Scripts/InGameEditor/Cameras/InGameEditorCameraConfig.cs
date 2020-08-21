using UnityEngine;
using UnityEngine.Serialization;
using UnityUtils.Constants;

namespace InGameEditor.Cameras
{
    [CreateAssetMenu(fileName = nameof(InGameEditorCameraConfig), menuName = MenuName.Data + nameof(InGameEditorCameraConfig))]
    public class InGameEditorCameraConfig : ScriptableObject
    {
        [FormerlySerializedAs("panningSpeed")] [Range(1, 1000f)] [SerializeField]
        private float maxPanningSpeed;

        [FormerlySerializedAs("bufferToEdge")] [Range(0, 10f)] [SerializeField]
        private float bufferToClampingEdge;

        [FormerlySerializedAs("edgePercentage")] [Range(0f, 1f)] [SerializeField]
        private float edgePercentageToPan;

        [Range(1f, 5f)] [SerializeField] private float smoothFactor;


        public float BufferToClampingEdge => bufferToClampingEdge;
        public float EdgePercentageToPan => edgePercentageToPan;
        public float MaxPanningSpeed => maxPanningSpeed;

        public float SmoothFactor => smoothFactor;
    }
}