using UnityEngine;
using UnityUtils.Constants;

namespace WorldConfigurations
{
    [CreateAssetMenu(fileName = nameof(WorldConfig), menuName = MenuName.Data + nameof(WorldConfig))]
    public class WorldConfig : ScriptableObject
    {
        [Range(0f, 10f)] [SerializeField] private float innerRadius;
        [SerializeField] private Vector3 upAxis;

        public Vector3 UpAxis => upAxis;
        public float InnerRadius => innerRadius;

        // 0.866025 -> sqrt(3) / 2, read https://catlikecoding.com/unity/tutorials/hex-map/part-1/, session "about hexagons" for details
        public float OuterRadius => innerRadius / 0.86602540378f;

        //Origin from center, begin from top, rotate clockwise
        public Vector3[] TileCorners => new[]
        {
            new Vector3(0f, 0f, OuterRadius),
            new Vector3(InnerRadius, 0f, 0.5f * OuterRadius),
            new Vector3(InnerRadius, 0f, -0.5f * OuterRadius),
            new Vector3(0f, 0f, -OuterRadius),
            new Vector3(-InnerRadius, 0f, -0.5f * OuterRadius),
            new Vector3(-InnerRadius, 0f, 0.5f * OuterRadius)
        };

        public static WorldConfig Create(Vector3 upAxis, float innerRadius)
        {
            var toReturn = CreateInstance<WorldConfig>();

            toReturn.upAxis = upAxis;
            toReturn.innerRadius = innerRadius;

            return toReturn;
        }
    }
}