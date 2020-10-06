using Main.Core.Game.Common.Factories;
using UnityEngine;
using UnityUtils.Constants;

namespace Main.Core.InGameEditor.Data.Availables
{
    [CreateAssetMenu(
        fileName = nameof(AvailableGameObjectProviders),
        menuName = MenuName.Data + "InGameEditor/" + nameof(AvailableGameObjectProviders)
    )]
    public class AvailableGameObjectProviders : AvailableSet<GameObjectFactory>
    {
    }
}