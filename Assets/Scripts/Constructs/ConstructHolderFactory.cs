using Common.Factories;
using UnityEngine;
using UnityUtils.Constants;

namespace Constructs
{
    [CreateAssetMenu(menuName = MenuName.Providers + "ConstructHolder", fileName = nameof(ConstructHolderFactory))]
    public class ConstructHolderFactory : PooledMonoBehaviourFactory<ConstructHolder>
    {
    }
}