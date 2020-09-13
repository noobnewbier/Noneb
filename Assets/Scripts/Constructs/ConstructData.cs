using Common.BoardItems;
using UnityEngine;

namespace Constructs
{
    [CreateAssetMenu(menuName = "Data/Construct", fileName = "ConstructData")]
    public class ConstructData : BoardItemData
    {
        //todo: add implementation related to stronghold
        //todo: implementation and design of construct
        [SerializeField] private string constructName;
        public override string DataName => constructName;
    }
}