using Common.Providers;
using UnityEngine;
using UnityUtils.Constants;

namespace Strongholds
{
    [CreateAssetMenu(menuName = MenuName.Providers + nameof(StrongholdHolder), fileName = nameof(StrongholdHolderProvider))]
    public class StrongholdHolderProvider : PooledMonoBehaviourProvider<StrongholdHolder>
    {
    }
}