using Common.Providers;
using UnityEngine;
using UnityUtils.Constants;

namespace Constructs
{
    [CreateAssetMenu(menuName = MenuName.Providers + "ConstructHolder", fileName = nameof(ConstructHolderProvider))]
    public class ConstructHolderProvider : PooledMonoBehaviourProvider<ConstructHolder>
    {
    }
}