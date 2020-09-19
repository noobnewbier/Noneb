﻿using System;
using Common.Providers;
using Constructs;
using UnityEngine;
using UnityUtils.Constants;

namespace GameEnvironments.Common.Repositories.BoardItems.Providers
{
    [CreateAssetMenu(fileName = nameof(ConstructsRepositoryProvider), menuName = MenuName.ScriptableRepository + "ConstructsRepository")]
    public class ConstructsRepositoryProvider : ScriptableObjectProvider<BoardItemsRepository<Construct>>
    {
        private readonly Lazy<BoardItemsRepository<Construct>> _lazyInstance =
            new Lazy<BoardItemsRepository<Construct>>(() => new BoardItemsRepository<Construct>());

        public override BoardItemsRepository<Construct> Provide()
        {
            return _lazyInstance.Value;
        }
    }
}