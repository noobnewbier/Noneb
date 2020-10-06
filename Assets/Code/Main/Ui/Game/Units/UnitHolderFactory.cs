using Main.Core.Game.Common.Factories;
using UnityEngine;
using UnityUtils.Constants;

namespace Main.Ui.Game.Units
{
    [CreateAssetMenu(menuName = MenuName.Providers + "UnitHolder", fileName = nameof(UnitHolderFactory))]
    public class UnitHolderFactory : PooledMonoBehaviourFactory<UnitHolder>
    {
    }
}