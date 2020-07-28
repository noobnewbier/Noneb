using UnityEngine;

namespace Common.Providers
{
    [CreateAssetMenu(fileName = nameof(GameObjectProvider), menuName = "Providers/GameObject")]
    public class GameObjectProvider : ScriptableObjectProvider<GameObject>
    {
        [SerializeField] private GameObject gameObject;
        
        public override GameObject Provide()
        {
            return Instantiate(gameObject);
        }
    }
}