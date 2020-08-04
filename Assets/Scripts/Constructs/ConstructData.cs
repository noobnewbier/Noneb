using Common.BoardItems;
using UnityEngine;

namespace Constructs
{
    [CreateAssetMenu(menuName = "Data/Construct", fileName = "ConstructData")]
    public class ConstructData :ScriptableObject, IBoardItemData
    {
        //todo: add implementation related to stronghold
        //todo: implementation and design of construct
        [SerializeField] private string constructName;

        public string ConstructName => constructName;
    }
}