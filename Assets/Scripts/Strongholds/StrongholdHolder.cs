using Common.Holders;
using UnityEditor;
using UnityEngine;

namespace Strongholds
{
    public class StrongholdHolder : MonoBehaviour, IHolder<Stronghold>
    {
        public Stronghold Value { get; private set; }

        public void Initialize(Stronghold stronghold)
        {
            Value = stronghold;
        }

        private void OnDrawGizmosSelected()
        {
            if (Value == null)
            {
                return;
            }

            var style = new GUIStyle {normal = {textColor = Color.red}};
            Handles.Label(
                transform.position,
                $"{Value.Construct.ConstructData.ConstructName} captured by : {Value.Unit.UnitData.UnitName}",
                style
            );
        }
    }
}