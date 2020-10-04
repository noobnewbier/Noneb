using Common.Factories;
using Common.Providers;
using UnityEngine;
using UnityUtils.Constants;

namespace Strongholds
{
    [CreateAssetMenu(menuName = MenuName.Providers + nameof(StrongholdHolder), fileName = nameof(StrongholdHolderFactory))]
    public class StrongholdHolderFactory : PooledMonoBehaviourFactory<StrongholdHolder>
    {
    }
}