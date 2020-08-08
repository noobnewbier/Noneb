using System;
using UnityEngine;

namespace WorldConfigurations
{
    [Serializable]
    public class WorldConfigurationJson
    {
        [SerializeField] private float innerRadius;
        [SerializeField] private Vector3 upAxis;

        public WorldConfigurationJson(float innerRadius, Vector3 upAxis)
        {
            this.innerRadius = innerRadius;
            this.upAxis = upAxis;
        }

        public float InnerRadius => innerRadius;

        public Vector3 UpAxis => upAxis;
    }
}