using UnityEngine;

namespace DebugUtils
{
    public class LineAngleDrawer : MonoBehaviour
    {
        [SerializeField] private Transform originTransform;
        [Range(0, 100f)] [SerializeField] private float distanceFromStartingPoint;
        [Range(-360f, 360f)] [SerializeField] private float polarAngle;
        [Range(-360f, 360f)] [SerializeField] private float elevationAngle;


        private void OnDrawGizmosSelected()
        {
            if (originTransform == null)
            {
                return;
            }

            var origin = originTransform.position;
            var endPoint = SphericalToCartesian(distanceFromStartingPoint, polarAngle * Mathf.Deg2Rad, elevationAngle * Mathf.Deg2Rad, origin);
            Gizmos.DrawLine(origin, endPoint);
        }

        private static Vector3 SphericalToCartesian(float radius, float polar, float elevation, Vector3 origin)
        {
            var a = radius * Mathf.Cos(elevation);
            var toReturn = new Vector3
            {
                x = a * Mathf.Cos(polar),
                y = radius * Mathf.Sin(elevation),
                z = a * Mathf.Sin(polar)
            };

            return toReturn + origin;
        }
    }
}