using Main.Core.Game.Common.Factories;
using UnityEngine;
using UnityUtils.Constants;

namespace Main.Ui.Game.Constructs
{
    [CreateAssetMenu(menuName = MenuName.Providers + "ConstructHolder", fileName = nameof(ConstructHolderFactory))]
    public class ConstructHolderFactory : PooledMonoBehaviourFactory<ConstructHolder>
    {
    }
}