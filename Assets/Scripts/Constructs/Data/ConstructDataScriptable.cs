using Common.BoardItems;
using UnityEngine;
using UnityUtils.Constants;

namespace Constructs.Data
{
    [CreateAssetMenu(menuName = MenuName.Data + nameof(Construct), fileName = nameof(ConstructDataScriptable))]
    public class ConstructDataScriptable : BoardItemDataScriptable
    {
        [SerializeField] private string constructName;

        public ConstructData ToData()
        {
            return new ConstructData(Icon, constructName, this);
        }
    }
}