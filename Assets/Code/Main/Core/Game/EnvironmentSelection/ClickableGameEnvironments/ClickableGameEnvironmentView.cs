using System;
using GameEnvironments.Common.Data;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace EnvironmentSelection.ClickableGameEnvironments
{
    public class ClickableGameEnvironmentView : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private TextMeshProUGUI environmentNameText;

        private GameEnvironment _gameEnvironment;
        private Action<GameEnvironment> _onClickEvent;

        public void OnPointerClick(PointerEventData eventData)
        {
            _onClickEvent.Invoke(_gameEnvironment);
        }

        public void Instantiate(GameEnvironment gameEnvironment, Action<GameEnvironment> onClickEvent)
        {
            _gameEnvironment = gameEnvironment;
            _onClickEvent = onClickEvent;

            ShowEnvironmentName();
        }

        private void ShowEnvironmentName()
        {
            environmentNameText.text = _gameEnvironment.EnvironmentName;
        }
    }
}