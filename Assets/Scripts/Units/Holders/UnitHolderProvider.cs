using Common.Providers;
using UnityEngine;
using UnityUtils.Constants;

namespace Units.Holders
{
    [CreateAssetMenu(menuName = MenuName.Providers + "UnitHolder", fileName = nameof(UnitHolderProvider))]
    public class UnitHolderProvider : ScriptableGameObjectAndComponentProvider<UnitHolder>
    {
    }
}