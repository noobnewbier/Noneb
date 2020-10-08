using Main.Core.Game.Common.BoardItems;
using UnityEngine;
using UnityUtils.Constants;

namespace Main.Core.Game.Constructs
{
    [CreateAssetMenu(menuName = MenuName.Data + nameof(Construct), fileName = nameof(ConstructDataScriptable))]
    public class ConstructDataScriptable : BoardItemDataScriptable
    {
        [SerializeField] private string constructName;

        public ConstructData ToData() => new ConstructData(Icon, constructName, this);
    }
}