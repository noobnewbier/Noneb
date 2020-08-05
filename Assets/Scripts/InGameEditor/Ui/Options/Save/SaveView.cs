using System;
using GameEnvironments.Common;
using GameEnvironments.Common.Repositories.CurrentGameEnvironment;
using InGameEditor.Repositories.SelectedEditorPalettes;
using TMPro;
using UniRx;
using UnityEngine;

namespace InGameEditor.Ui.Options.Save
{
    public class SaveView : MonoBehaviour
    {
        [SerializeField] private SaveType saveType;

        [SerializeField] private GameObject savingDetailDialog;
        [SerializeField] private SaveViewModelFactory viewModelFactory;
        [SerializeField] private SelectedEditorPaletteRepositoryProvider editorPaletteRepositoryProvider;
        [SerializeField] private CurrentGameEnvironmentRepositoryProvider currentGameEnvironmentRepositoryProvider;
        [SerializeField] private TMP_InputField filenameInputField;

        private SaveViewModel _viewModel;
        private CompositeDisposable _compositeDisposable;

        private void OnEnable()
        {
            _compositeDisposable = new CompositeDisposable();
            _viewModel = viewModelFactory.Create(
                currentGameEnvironmentRepositoryProvider.Provide(),
                saveType
            );

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
            _viewModel.ShowEditorMessage("File already exist, pick another filename");
        }

        private void ShowErrorMessage()
        {
            _viewModel.ShowEditorMessage("File already exist, pick another filename");
        }

        private void ShowSavingSuccess()
        {
            _viewModel.ShowEditorMessage("File already exist, pick another filename");
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