﻿namespace Main.Core.Game.Common.Providers
{
    public interface IObjectProvider<out T>
    {
        T Provide();
    }
}