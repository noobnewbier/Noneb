﻿using UnityEngine;

namespace Common.Providers
{
    public abstract class MonoObjectProvider<T> : MonoBehaviour, IObjectProvider<T>
    {
        public abstract T Provide();
    }
}