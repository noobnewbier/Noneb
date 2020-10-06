using Main.Core.Game.Common.Factories;
using UnityEngine;
using UnityUtils.Constants;

namespace Main.Ui.Game.Strongholds
{
    [CreateAssetMenu(menuName = MenuName.Providers + nameof(StrongholdHolder), fileName = nameof(StrongholdHolderFactory))]
    public class StrongholdHolderFactory : PooledMonoBehaviourFactory<StrongholdHolder>
    {
    }
}