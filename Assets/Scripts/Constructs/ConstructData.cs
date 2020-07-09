using UnityEngine;

namespace Constructs
{
    [CreateAssetMenu(menuName = "Data/Construct", fileName = "ConstructData")]
    public class ConstructData :ScriptableObject
    {
        //todo: implementation and design of construct
        [SerializeField] private string constructName;

        public string ConstructName => constructName;
    }
}