using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace DebugUtils
{
    public class CameraViewDrawer : MonoBehaviour
    {
        [SerializeField] private Transform originTransform;
        [SerializeField] private Camera drawnCamera;
        [SerializeField] private float planeYPosition;

        private void OnDrawGizmosSelected()
        {
            if (drawnCamera == null || originTransform == null)
            {
                return;
            }

            var frustumCorners = GetFrustumCorners();

            DrawLineFromCamera(frustumCorners);
            DrawIntersectionPlane(planeYPosition, frustumCorners);
        }

        private void DrawLineFromCamera(IReadOnlyList<Vector3> frustumCorners)
        {
            var originalColor = Gizmos.color;
            Gizmos.color = Color.red;

            for (var i = 0; i < 4; i++) Gizmos.DrawLine(drawnCamera.transform.position, frustumCorners[i]);

            Gizmos.color = originalColor;
        }


        private void DrawIntersectionPlane(float targetYPosition, IReadOnlyList<Vector3> cameraFrustumCorners)
        {
            var originalColor = Gizmos.color;
            Gizmos.color = Color.blue;
            var cameraPosition = drawnCamera.transform.position;
            var intersectionCorners = new Vector3[cameraFrustumCorners.Count];

            for (var i = 0; i < cameraFrustumCorners.Count; i++)
            {
                var (x, z) = GetXzGivenYFromLineEquation(targetYPosition, cameraPosition, cameraFrustumCorners[i]);
                intersectionCorners[i] = new Vector3(x, targetYPosition, z);
                
                Handles.Label(intersectionCorners[i], i.ToString());
            }

            for (var i = 0; i < intersectionCorners.Length - 1; i++)
            {
                Gizmos.DrawLine(intersectionCorners[i], intersectionCorners[i + 1]);
            }
            Gizmos.DrawLine(intersectionCorners[0], intersectionCorners.Last());

            Gizmos.color = originalColor;
        }

        private Vector3[] GetFrustumCorners()
        {
            var cameraTransform = drawnCamera.transform;
            var frustumCorners = new Vector3[4];
            drawnCamera.CalculateFrustumCorners(
                new Rect(0, 0, 1, 1),
                drawnCamera.farClipPlane,
                Camera.MonoOrStereoscopicEye.Mono,
                frustumCorners
            );

            return frustumCorners.Select(c => cameraTransform.TransformVector(c) + cameraTransform.position).ToArray();
        }


        //reference: https://math.stackexchange.com/questions/404440/what-is-the-equation-for-a-3d-line symmeteric form
        private static (float x, float z) GetXzGivenYFromLineEquation(float targetY, Vector3 origin, Vector3 endPoint)
        {
            var direction = endPoint - origin;

            var yEquationAnswer = (targetY - origin.y) / direction.y;

            var x = direction.x * yEquationAnswer + origin.x;
            var z = direction.z * yEquationAnswer + origin.z;

            return (x, z);
        }
    }
}