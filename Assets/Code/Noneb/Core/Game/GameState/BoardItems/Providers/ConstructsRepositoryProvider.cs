using System;
using Noneb.Core.Game.Common.Providers;
using Noneb.Core.Game.Constructs;
using UnityEngine;
using UnityUtils.Constants;

namespace Noneb.Core.Game.GameState.BoardItems.Providers
{
    [CreateAssetMenu(fileName = nameof(ConstructsRepositoryProvider), menuName = MenuName.ScriptableRepository + "ConstructsRepository")]
    public class ConstructsRepositoryProvider : ScriptableObject, IObjectProvider<IBoardItemsRepository<Construct>>
    {
        private readonly Lazy<BoardItemsRepository<Construct>> _lazyInstance =
            new Lazy<BoardItemsRepository<Construct>>(() => new BoardItemsRepository<Construct>());

        public IBoardItemsRepository<Construct> Provide() => _lazyInstance.Value;
    }
}