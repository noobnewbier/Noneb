using UnityEngine;
using UnityUtils.Constants;

namespace Common.Factories
{
    [CreateAssetMenu(fileName = nameof(GameObjectFactory), menuName = MenuName.Providers + "GameObject")]
    public class GameObjectFactory : ScriptableObject, IFactory<GameObject>
    {
        [SerializeField] private GameObject gameObject;

        public GameObject Create()
        {
            return Instantiate(gameObject);
        }

        public GameObject Create(Transform parentTransform, bool instantiateInWorldSpace = true)
        {
            return Instantiate(gameObject, parentTransform, instantiateInWorldSpace);
        }
    }
}