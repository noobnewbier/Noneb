using System;
using Main.Core.Game.Common.Providers;
using Main.Core.Game.Constructs;
using UnityEngine;
using UnityUtils.Constants;

namespace Main.Core.Game.GameState.BoardItems.Providers
{
    [CreateAssetMenu(fileName = nameof(ConstructsRepositoryProvider), menuName = MenuName.ScriptableRepository + "ConstructsRepository")]
    public class ConstructsRepositoryProvider : ScriptableObject, IObjectProvider<IBoardItemsRepository<Construct>>
    {
        private readonly Lazy<BoardItemsRepository<Construct>> _lazyInstance =
            new Lazy<BoardItemsRepository<Construct>>(() => new BoardItemsRepository<Construct>());

        public IBoardItemsRepository<Construct> Provide() => _lazyInstance.Value;
    }
}