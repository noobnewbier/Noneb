using System;
using Main.Core.Game.Common.Providers;
using Main.Core.Game.Constructs;
using UnityEngine;
using UnityUtils.Constants;

namespace Main.Core.Game.GameEnvironments.BoardItems.Providers
{
    [CreateAssetMenu(fileName = nameof(ConstructsRepositoryProvider), menuName = MenuName.ScriptableRepository + "ConstructsRepository")]
    public class ConstructsRepositoryProvider : ScriptableObjectProvider<BoardItemsRepository<Construct>>
    {
        private readonly Lazy<BoardItemsRepository<Construct>> _lazyInstance =
            new Lazy<BoardItemsRepository<Construct>>(() => new BoardItemsRepository<Construct>());

        public override BoardItemsRepository<Construct> Provide() => _lazyInstance.Value;
    }
}