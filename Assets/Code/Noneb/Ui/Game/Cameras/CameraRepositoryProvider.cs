using System;
using Noneb.Core.Game.Common.Providers;
using UnityEngine;
using UnityUtils.Constants;

namespace Noneb.Ui.Game.Cameras
{
    [CreateAssetMenu(
        fileName = nameof(CameraRepositoryProvider),
        menuName = MenuName.ScriptableRepository + nameof(CameraRepository)
    )]
    public class CameraRepositoryProvider : ScriptableObject, IObjectProvider<CameraRepository>
    {
        private readonly Lazy<CameraRepository> _lazyInstance =
            new Lazy<CameraRepository>(() => new CameraRepository());

        public CameraRepository Provide() => _lazyInstance.Value;
    }
}