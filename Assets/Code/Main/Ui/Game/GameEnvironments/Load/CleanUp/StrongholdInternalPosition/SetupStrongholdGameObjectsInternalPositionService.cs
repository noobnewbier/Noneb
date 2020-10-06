using UnityEngine;
using UnityUtils.PositionProviders;

namespace Main.Ui.Game.GameEnvironments.Load.CleanUp.StrongholdInternalPosition
{
    /// <summary>
    /// Setting up unit gameObject's position within a stronghold
    /// We assume that EVERY CONSTRUCT'S has a location provider in the root, without it this would not work.
    /// </summary>
    public interface ISetupStrongholdGameObjectsInternalPositionService
    {
        void SetPosition(GameObject unitGameObject, GameObject constructGameObject);
    }

    public class SetupStrongholdGameObjectsInternalPositionService : ISetupStrongholdGameObjectsInternalPositionService
    {
        public void SetPosition(GameObject unitGameObject, GameObject constructGameObject)
        {
            var locationProvider = constructGameObject.GetComponent<PositionProvider>();

            unitGameObject.transform.position = locationProvider.ProvideLocation();
        }
    }
}