﻿using UnityEngine;

namespace Noneb.Ui.Game.Cameras
{
    public class CameraRepositorySetter : MonoBehaviour
    {
        [SerializeField] private CameraRepositoryProvider provider;
        [SerializeField] private Camera editorCamera;

        [ContextMenu(nameof(Set))]
        public void Set()
        {
            provider.Provide().Set(editorCamera);
        }
    }
}