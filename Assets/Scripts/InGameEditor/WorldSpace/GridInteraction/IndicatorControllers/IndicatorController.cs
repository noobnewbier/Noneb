using JetBrains.Annotations;
using Tiles.Holders;
using UnityEngine;

namespace InGameEditor.WorldSpace.GridInteraction.IndicatorControllers
{
    public class IndicatorController : MonoBehaviour
    {
        [SerializeField] private GameObject indicatorGameObject;
        [SerializeField] private IndicatorControllerConfig config;

        public void ShowIndicator([CanBeNull] TileHolder tileHolder)
        {
            if (tileHolder == null)
            {
                gameObject.SetActive(false);
                return;
            }

            var indicatorPosition = tileHolder.transform.position;
            indicatorPosition.y += config.IndicatorOffsetFromMap;

            indicatorGameObject.transform.position = indicatorPosition;
            indicatorGameObject.SetActive(true);
        }

        public void SetDisabled()
        {
            indicatorGameObject.SetActive(false);
        }
    }
}