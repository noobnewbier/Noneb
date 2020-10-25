using System.Linq;
using UnityEngine;
using UnityUtils;

// ReSharper disable MemberCanBeMadeStatic.Local
/*
 * anticipating that there will be dependencies in the future
 * (if we put both map transform and editor camera into its own repository like how we do it in GameEnv)
 */

namespace Noneb.Ui.InGameEditor.Cameras.SizeInView
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
                var (x, z) = LineEquations.GetXzGivenY(targetYPosition, cameraPosition, cameraFrustumCorners[i]);
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
    }
}