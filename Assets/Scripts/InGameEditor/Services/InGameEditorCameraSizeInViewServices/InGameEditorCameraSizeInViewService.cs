using System.Linq;
using UnityEngine;

// ReSharper disable MemberCanBeMadeStatic.Local
/*
 * anticipating that there will be dependencies in the future
 * (if we put both map transform and editor camera into its own repository like how we do it in GameEnv)
 */

namespace InGameEditor.Services.InGameEditorCameraSizeInViewServices
{
    public interface IInGameEditorCameraSizeInViewService
    {
        (float minWidth, float distanceToTop, float distanceToBottom) GetViewDistanceToFrustumOnPlaneInWorldSpace(
            Camera editorCamera,
            Transform mapTransform);
    }

    public class InGameEditorCameraSizeInViewService : IInGameEditorCameraSizeInViewService
    {
        /// <summary>
        /// Finding 4 intersection point of camera frustum on the infinite plane(at mapTransform's height).
        /// Returning the maximum size of a rect that it can be bounded within the 4 intersection point.
        /// </summary>
        public (float minWidth, float distanceToTop, float distanceToBottom) GetViewDistanceToFrustumOnPlaneInWorldSpace(
            Camera editorCamera,
            Transform mapTransform)
        {
            var cameraFrustumCorners = GetFrustumCorners(editorCamera);
            var cameraPosition = editorCamera.transform.position;
            var targetYPosition = mapTransform.position.y;
            var intersectionCorners = new Vector3[cameraFrustumCorners.Length];

            for (var i = 0; i < cameraFrustumCorners.Length; i++)
            {
                var (x, z) = GetXzGivenYFromLineEquation(targetYPosition, cameraPosition, cameraFrustumCorners[i]);
                intersectionCorners[i] = new Vector3(x, targetYPosition, z);
            }

            var distanceToTop = intersectionCorners[1].z - cameraPosition.z;
            var distanceToBottom = cameraPosition.z - intersectionCorners[0].z;
            var minWidth = intersectionCorners[3].x - intersectionCorners[0].x;

            return (minWidth, distanceToTop, distanceToBottom);
        }

        private Vector3[] GetFrustumCorners(Camera editorCamera)
        {
            var cameraTransform = editorCamera.transform;
            var frustumCorners = new Vector3[4];
            editorCamera.CalculateFrustumCorners(
                new Rect(0, 0, 1, 1),
                editorCamera.farClipPlane,
                Camera.MonoOrStereoscopicEye.Mono,
                frustumCorners
            );

            return frustumCorners.Select(c => cameraTransform.TransformVector(c) + cameraTransform.position).ToArray();
        }

        //reference: https://math.stackexchange.com/questions/404440/what-is-the-equation-for-a-3d-line symmetric form
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