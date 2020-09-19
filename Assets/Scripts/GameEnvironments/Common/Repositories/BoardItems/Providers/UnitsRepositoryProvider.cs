using System;
using Common.Providers;
using Units;
using UnityEngine;
using UnityUtils.Constants;

namespace GameEnvironments.Common.Repositories.BoardItems.Providers
{
    [CreateAssetMenu(fileName = nameof(UnitsRepositoryProvider), menuName = MenuName.ScriptableRepository + "UnitsRepository")]
    public class UnitsRepositoryProvider : ScriptableObjectProvider<BoardItemsRepository<Unit>>
    {
        private readonly Lazy<BoardItemsRepository<Unit>>
            _lazyInstance = new Lazy<BoardItemsRepository<Unit>>(() => new BoardItemsRepository<Unit>());

        public override BoardItemsRepository<Unit> Provide()
        {
            return _lazyInstance.Value;
        }
    }
}