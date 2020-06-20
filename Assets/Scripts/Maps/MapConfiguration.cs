using UnityEngine;

namespace Maps
{
    [CreateAssetMenu]
    public class MapConfiguration : ScriptableObject
    {
        [Range(0f, 10f)] [SerializeField] private float innerRadius;
        [SerializeField] private HexagonOrientation orientation;
        [Range(1, 10)] [SerializeField] private int xSize;
        [Range(1, 10)] [SerializeField] private int zSize;
        [SerializeField] private Vector3 upAxis;

        public Vector3 UpAxis => upAxis;
        public HexagonOrientation Orientation => orientation;
        public int XSize => xSize;
        public int ZSize => zSize;
        public float InnerRadius => innerRadius;
        // 0.866025 -> sqrt(3) / 2, read https://catlikecoding.com/unity/tutorials/hex-map/part-1/, session "about hexagons" for details
        public float OuterRadius => innerRadius / 0.86602540378f;
    }
}