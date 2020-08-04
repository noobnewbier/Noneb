using UnityEngine;
using UnityUtils.Constants;

namespace Common.Providers
{
    [CreateAssetMenu(fileName = nameof(GameObjectProvider), menuName = MenuName.Providers + "GameObject")]
    public class GameObjectProvider : ScriptableObjectProvider<GameObject>, IGameObjectProvider
    {
        [SerializeField] private GameObject gameObject;

        public override GameObject Provide()
        {
            return Instantiate(gameObject);
        }

        public GameObject Provide(Transform parentTransform, bool instantiateInWorldSpace = true)
        {
            return Instantiate(gameObject, parentTransform, instantiateInWorldSpace);
        }
    }
}