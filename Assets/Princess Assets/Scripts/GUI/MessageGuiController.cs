using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

namespace PrincessAdventure
{
    public class MessageGuiController : MonoBehaviour
    {

        [SerializeField] private GameObject _continueButton;
        [SerializeField] private GameObject _cancelButton;
        [SerializeField] private TextMeshProUGUI _msgText;
        [SerializeField] private Text _titleText;
        [SerializeField] private TextMeshProUGUI _buttonText;


        [SerializeField] private GameObject _wishBg;
        [SerializeField] private GameObject _wishIcon;
        [SerializeField] private GameObject _titleIcon;
        [SerializeField] private GameObject _tipIcon;

        [SerializeField] private AudioClip _click;
        [SerializeField] private List<AudioClip> _heartReadSfx;

        private MessageModels.Message _currentMsg;
        private int _msgIndex = 0;
        private float _dialogDelay = 0.03f;
        private string _currentDialogText = "";
        private string _fullText;

        public void LoadMessageGui(MessageModels.Message msg, string title)
        {

            _currentMsg = msg;
            _msgIndex = 0;

            _fullText = GameManager.GameInstance.GetLevelMessage(msg.messageIds[_msgIndex]);
            _titleText.text = title;
            SetupMessageGui(); 


            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(_continueButton);

        }

        private void SetupMessageGui()
        {
            SetupDisplay();
            SetupResponseButton();

        }

        private void SetupDisplay()
        {


            switch (_currentMsg.display)
            {
                case MessageModels.DisplayType.Dialog:
                    _wishBg.SetActive(false);
                    _wishIcon.SetActive(false);
                    _tipIcon.SetActive(false);
                    _titleIcon.SetActive(true);
                    _currentDialogText = "";
                    _msgText.alignment = TextAlignmentOptions.TopLeft;
                    StartCoroutine(ShowDialogText());
                    break;
                case MessageModels.DisplayType.Sign:
                case MessageModels.DisplayType.Tip:
                    _wishBg.SetActive(false);
                    _wishIcon.SetActive(false);
                    _tipIcon.SetActive(true);
                    _titleIcon.SetActive(false);
                    _msgText.alignment = TextAlignmentOptions.Center;
                    _msgText.text = _fullText;
                    break;
                case MessageModels.DisplayType.Heart:
                    PlayHeartSfx();
                    _wishBg.SetActive(true);
                    _wishIcon.SetActive(true);
                    _tipIcon.SetActive(false);
                    _titleIcon.SetActive(false);
                    _currentDialogText = "";
                    _msgText.alignment = TextAlignmentOptions.TopLeft;
                    StartCoroutine(ShowDialogText());
                    break;
            }
        }

        private void SetupResponseButton()
        {
            if (_msgIndex == _currentMsg.messageIds.Count - 1)
            {
                switch (_currentMsg.response.responseType)
                {
                    case MessageModels.ResponseTypes.Ok:
                        _cancelButton.SetActive(false);
                        _buttonText.text = "OK";
                        break;
                    case MessageModels.ResponseTypes.GiveItem:
                        _cancelButton.SetActive(true);
                        _buttonText.text = "Take";
                        break;
                    case MessageModels.ResponseTypes.Shop:
                        _cancelButton.SetActive(true);
                        _buttonText.text = "Shop";
                        break;
                    case MessageModels.ResponseTypes.UpdateQuest:
                        _cancelButton.SetActive(true);
                        _buttonText.text = "Wish";
                        break;
                }
            }
            else
                _buttonText.text = "...";
        }

        public void ResponseButtonClick()
        {
            if (_msgIndex == _currentMsg.messageIds.Count - 1)
            {
                switch (_currentMsg.response.responseType)
                {
                    case MessageModels.ResponseTypes.Ok:
                        ContinueGame();
                        break;
                    case MessageModels.ResponseTypes.GiveItem:
                        switch (_currentMsg.response.typeOrStage)
                        {
                            case 0:
                                GameManager.GameInstance.PickupMajorTreasure(MajorTreasures.Friend, _currentMsg.response.id);
                                break;
                            case 1:
                                GameManager.GameInstance.PickupMajorTreasure(MajorTreasures.Hat, _currentMsg.response.id);
                                break;
                            case 2:
                                GameManager.GameInstance.PickupMajorTreasure(MajorTreasures.Necklace, _currentMsg.response.id);
                                break;
                            case 3:
                                GameManager.GameInstance.PickupMajorTreasure(MajorTreasures.Outfit, _currentMsg.response.id);
                                break;
                            case 4:
                                GameManager.GameInstance.PickupMajorTreasure(MajorTreasures.Ring, _currentMsg.response.id);
                                break;
                            case 5:
                                GameManager.GameInstance.PickupMajorTreasure(MajorTreasures.Shoes, _currentMsg.response.id);
                                break;

                        }
                        break;
                    case MessageModels.ResponseTypes.Shop:
                        ContinueGame();
                        break;
                    case MessageModels.ResponseTypes.UpdateQuest:
                        ContinueGame();
                        break;
                }
            }
            else
            {
                _msgIndex++;
                SetupResponseButton();
                _msgText.text = GameManager.GameInstance.GetLevelMessage(_currentMsg.messageIds[_msgIndex]);

            }

        }

        public void ContinueGame()
        {
            SoundManager.SoundInstance.PlayEffectSound(_click);
            _fullText = "";

            GameManager.GameInstance.ResumeGameplay();
        }

        IEnumerator ShowDialogText()
        {
            //Debug.Log(_fullText);
            //Debug.Log(_fullText.Length);
            for (int i = 0; i <= _fullText.Length; i++)
            {
                //Debug.Log(i);
                //Debug.Log(_currentDialogText);

                _currentDialogText = _fullText.Substring(0, i);
                _msgText.text = _currentDialogText;
                yield return new WaitForSecondsRealtime(_dialogDelay);
            }
        }

        private void PlayHeartSfx()
        {
            int soundIndex = Random.Range(0, _heartReadSfx.Count);
            SoundManager.SoundInstance.PlayEffectSound(_heartReadSfx[soundIndex]);

        }
    }
}
