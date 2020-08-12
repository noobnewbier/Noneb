﻿using System;
using Common.Providers;
using UnityEngine;
using UnityUtils.Constants;

namespace GameEnvironments.Common.Services.GetEnvironmentFilenameServices
{
    [CreateAssetMenu(
        fileName = nameof(GetEnvironmentFilenameServiceProvider),
        menuName = MenuName.ScriptableService + nameof(GetEnvironmentFilenameService)
    )]
    public class GetEnvironmentFilenameServiceProvider : ScriptableObjectProvider<IGetEnvironmentFilenameService>
    {
        private readonly Lazy<IGetEnvironmentFilenameService> _lazyInstance =
            new Lazy<IGetEnvironmentFilenameService>(() => new GetEnvironmentFilenameService());

        public override IGetEnvironmentFilenameService Provide()
        {
            return _lazyInstance.Value;
        }
    }
}