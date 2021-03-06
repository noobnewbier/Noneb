﻿using Noneb.Core.Game.Common;
using UnityEngine;

namespace Noneb.Ui.Game.Cameras
{
    public interface ICameraGetRepository : IDataGetRepository<Camera>
    {
    }

    public interface ICameraSetRepository : IDataSetRepository<Camera>
    {
    }

    public class CameraRepository : DataRepository<Camera>, ICameraGetRepository, ICameraSetRepository
    {
    }
}