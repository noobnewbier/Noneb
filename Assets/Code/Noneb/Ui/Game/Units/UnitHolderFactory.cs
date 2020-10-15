using Noneb.Core.Game.Common.Factories;
using UnityEngine;
using UnityUtils.Constants;

namespace Noneb.Ui.Game.Units
{
    [CreateAssetMenu(menuName = MenuName.Providers + "UnitHolder", fileName = nameof(UnitHolderFactory))]
    public class UnitHolderFactory : PooledMonoBehaviourFactory<UnitHolder>
    {
    }
}