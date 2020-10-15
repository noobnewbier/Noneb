using JetBrains.Annotations;
using Noneb.Ui.Game.Tiles;
using UnityEngine;

namespace Noneb.Ui.InGameEditor.WorldSpace.GridInteraction.IndicatorControllers
{
    public class IndicatorController : MonoBehaviour
    {
        [SerializeField] private GameObject indicatorGameObject;
        [SerializeField] private IndicatorControllerConfig config;

        public void ShowIndicator([CanBeNull] TileHolder tileHolder)
        {
            if (tileHolder == null)
            {
                SetDisabled();
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