using UnityEngine;
using UnityUtils.Constants;

namespace Noneb.Core.Game.Common.Factories
{
    [CreateAssetMenu(fileName = nameof(GameObjectFactory), menuName = MenuName.Providers + "GameObject")]
    public class GameObjectFactory : ScriptableObject, IFactory<GameObject>
    {
        [SerializeField] private GameObject gameObject;

        public GameObject Create() => Instantiate(gameObject);

        public GameObject Create(Transform parentTransform, bool instantiateInWorldSpace = true) =>
            Instantiate(gameObject, parentTransform, instantiateInWorldSpace);
    }
}