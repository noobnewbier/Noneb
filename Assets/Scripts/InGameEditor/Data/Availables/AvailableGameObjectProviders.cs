using Common.Providers;
using UnityEngine;
using UnityUtils.Constants;

namespace InGameEditor.Data.Availables
{
    [CreateAssetMenu(
        fileName = nameof(AvailableGameObjectProviders),
        menuName = MenuName.Data + "InGameEditor/" + nameof(AvailableGameObjectProviders)
    )]
    public class AvailableGameObjectProviders : AvailableSet<GameObjectProvider>
    {
    }
}