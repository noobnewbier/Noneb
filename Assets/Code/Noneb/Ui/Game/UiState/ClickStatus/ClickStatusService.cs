using System;
using System.Collections.Generic;
using System.Linq;
using Noneb.Core.Game.Common;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Noneb.Ui.Game.UiState.ClickStatus
{
    public interface IClickStatusService : IDisposable
    {
        bool IsMouseClickOnUi(Vector3 mousePosition);
        bool IsMouseClickOnMap(Vector3 mousePosition);
    }

    public class ClickStatusService : IClickStatusService
    {
        private readonly IDisposable _disposable;

        private GraphicRaycaster _graphicRaycaster;
        private EventSystem _eventSystem;

        public ClickStatusService(IDataGetRepository<GraphicRaycaster> graphicRaycasterGetRepository, IDataGetRepository<EventSystem> eventSystemGetRepository)
        {
            _disposable = new CompositeDisposable
            {
                graphicRaycasterGetRepository.GetObservableStream()
                    .SubscribeOn(Scheduler.ThreadPool)
                    .ObserveOn(Scheduler.MainThread)
                    .Subscribe(raycaster => _graphicRaycaster = raycaster),
                
                eventSystemGetRepository.GetObservableStream()
                    .SubscribeOn(Scheduler.ThreadPool)
                    .ObserveOn(Scheduler.MainThread)
                    .Subscribe(eventSystem => _eventSystem = eventSystem)
            };
        }

        public bool IsMouseClickOnUi(Vector3 mousePosition)
        {
            var raycastResult = new List<RaycastResult>();
            var eventData = new PointerEventData(_eventSystem) {position = mousePosition};

            _graphicRaycaster.Raycast(eventData, raycastResult);

            return raycastResult.Any();
        }

        public bool IsMouseClickOnMap(Vector3 mousePosition) => throw new System.NotImplementedException();

        public void Dispose()
        {
            _disposable?.Dispose();
        }
    }
}