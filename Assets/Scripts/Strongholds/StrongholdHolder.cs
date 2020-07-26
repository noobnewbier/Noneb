using Common.Holders;
using Constructs;
using UnityEditor;
using UnityEngine;

namespace Strongholds
{
    public class StrongholdHolder : MonoBehaviour, IHolder<Stronghold>
    {
        public Stronghold Stronghold { get; private set; }

        public void Initialize(Stronghold stronghold)
        {
            Stronghold = stronghold;
        }
        
        private void OnDrawGizmosSelected()
        {
            if (Stronghold == null)
            {
                return;
            }

            var style = new GUIStyle {normal = {textColor = Color.red}};
            Handles.Label(
                transform.position,
                $"{Stronghold.Construct.ConstructData.ConstructName} captured by : {Stronghold.Unit.UnitData.UnitName}",
                style
            );
        }
    }
}