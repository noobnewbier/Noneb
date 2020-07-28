using Common.Providers;
using UnityEngine;

namespace InGameEditor.WorldSpace.Maps
{
    [CreateAssetMenu(fileName = nameof(MapViewModelProvider), menuName = "Providers/MapViewModel")]
    public class MapViewModelProvider : ScriptableObjectProvider<MapViewModel>
    {
        public override MapViewModel Provide()
        {
            throw new System.NotImplementedException();
        }
    }
}