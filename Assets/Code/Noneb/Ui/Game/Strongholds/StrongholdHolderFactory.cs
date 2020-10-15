using Noneb.Core.Game.Common.Factories;
using UnityEngine;
using UnityUtils.Constants;

namespace Noneb.Ui.Game.Strongholds
{
    [CreateAssetMenu(menuName = MenuName.Providers + nameof(StrongholdHolder), fileName = nameof(StrongholdHolderFactory))]
    public class StrongholdHolderFactory : PooledMonoBehaviourFactory<StrongholdHolder>
    {
    }
}