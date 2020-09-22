using Common.BoardItems;
using UnityEngine;

namespace Constructs.Data
{
    [CreateAssetMenu(menuName = "Data/Construct", fileName = "ConstructData")]
    public class ConstructDataScriptable : BoardItemDataScriptable
    {
        //todo: add implementation related to stronghold
        //todo: implementation and design of construct
        [SerializeField] private string constructName;

        public  ConstructData ToData()
        {
            return new ConstructData(Icon, constructName, this);
        }
    }
}