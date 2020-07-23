using System;
using EventManagement;
using EventManagement.Providers;
using InGameEditor.Events;
using InGameEditor.Repositories.GameEnvironments;
using InGameEditor.Repositories.SelectedEditorPalettes;
using InGameEditor.Services.SaveEnvironment;
using TMPro;
using UniRx;
using UnityEngine;

namespace InGameEditor.Ui.Options.Save
{
    public class SaveView : MonoBehaviour
    {
        [SerializeField] private GameObject savingDetailDialog;
        [SerializeField] private SaveViewModelFactory viewModelFactory;
        [SerializeField] private SelectedEditorPaletteRepositoryProvider editorPaletteRepositoryProvider;
        [SerializeField] private CurrentGameEnvironmentRepositoryProvider currentGameEnvironmentRepositoryProvider;
        [SerializeField] private TMP_InputField filenameInputField;
        [SerializeField] private EventAggregatorProvider uiEventAggregatorProvider;

        private SaveViewModel _viewModel;
        private IEventAggregator _uiEventAggregator;
        private CompositeDisposable _compositeDisposable;

        private void OnEnable()
        {
            _compositeDisposable = new CompositeDisposable();
            _viewModel = viewModelFactory.Create(
                editorPaletteRepositoryProvider.Provide().Palette,
                currentGameEnvironmentRepositoryProvider.Provide()
            );
            _uiEventAggregator = uiEventAggregatorProvider.ProvideEventAggregator();

            _compositeDisposable.Add(
                _viewModel.SavingResultEvent.Subscribe(
                    result =>
                    {
                        switch (result)
                        {
                            case SavingResult.Success:
                                ShowSavingSuccess();
                                break;
                            case SavingResult.FileExist:
                                ShowFileExist();
                                break;
                            case SavingResult.Error:
                                ShowErrorMessage();
                                break;
                            default:
                                throw new ArgumentOutOfRangeException(nameof(result), result, null);
                        }
                    }
                )
            );

            _compositeDisposable.Add(
                _viewModel.SavingDetailDialogVisibilityEvent.Subscribe(
                    visible =>
                    {
                        if (visible)
                        {
                            ShowSavingDetailDialog();
                        }
                        else
                        {
                            HideSavingDetailDialog();
                        }
                    }
                )
            );
        }

        private void ShowFileExist()
        {
            _uiEventAggregator.Publish(new UiMessageEvent("File already exist, pick another filename"));
        }

        private void ShowErrorMessage()
        {
            _uiEventAggregator.Publish(new UiMessageEvent("Error occurs while trying to save the environment"));
        }

        private void ShowSavingSuccess()
        {
            _uiEventAggregator.Publish(new UiMessageEvent("Success! Finally Noob you did something"));
        }

        private void ShowSavingDetailDialog()
        {
            savingDetailDialog.SetActive(true);
        }

        private void HideSavingDetailDialog()
        {
            savingDetailDialog.SetActive(false);
        }

        public void OnClickSaveMenuButton()
        {
            _viewModel.ClickedSaveMenuButton();
        }

        public void OnClickSaveInSavingDetailDialog()
        {
            _viewModel.Save(filenameInputField.text);
        }

        public void OnClickCancelInSavingDetailDialog()
        {
            _viewModel.ClickedCancelInSavingDetail();
        }

        private void OnDisable()
        {
            _compositeDisposable.Dispose();
        }
    }
}