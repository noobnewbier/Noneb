using System;
using Noneb.Core.Game.GameEnvironments.Save;
using Noneb.Core.Game.GameState.GameEnvironments;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.Serialization;

namespace Noneb.Ui.InGameEditor.Save
{
    public class SaveView : MonoBehaviour
    {
        [SerializeField] private GameObject savingDetailDialog;
        [SerializeField] private SaveViewModelFactory viewModelFactory;
        [SerializeField] private TMP_InputField filenameInputField;

        private SaveViewModel _viewModel;
        private CompositeDisposable _compositeDisposable;

        private void OnEnable()
        {
            _compositeDisposable = new CompositeDisposable();
            _viewModel = viewModelFactory.Create();

            _compositeDisposable.Add(
                _viewModel.savingResultEvent.Subscribe(
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
                _viewModel.savingDetailDialogVisibilityEvent.Subscribe(
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
            _viewModel.Dispose();
        }
    }
}