using System;
using Main.Core.Game.GameEnvironments.Data;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Main.Ui.InGameEditor.EnvironmentSelection.ClickableGameEnvironments
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

        public void Init(GameEnvironment gameEnvironment, Action<GameEnvironment> onClickEvent)
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