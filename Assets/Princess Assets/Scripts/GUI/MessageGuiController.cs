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
        [SerializeField] private TextMeshProUGUI _msgText;
        [SerializeField] private AudioClip _click;
        [SerializeField] private TextMeshProUGUI _buttonText;

        private MessageModels.Message _currentMsg;
        private int _msgIndex = 0;

        public void LoadMessageGui(MessageModels.Message msg)
        {

            _currentMsg = msg;
            _msgIndex = 0;

            SetupMessageGui(); 

            _msgText.text = GameManager.GameInstance.GetLevelMessage(msg.messageIds[_msgIndex]);

            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(_continueButton);

        }

        private void SetupMessageGui()
        {
            SetupResponseButton();

        }


        private void SetupResponseButton()
        {
            if (_msgIndex == _currentMsg.messageIds.Count - 1)
            {
                switch (_currentMsg.response.responseType)
                {
                    case MessageModels.ResponseTypes.Ok:
                        _buttonText.text = "OK";
                        break;
                    case MessageModels.ResponseTypes.GiveItem:
                        _buttonText.text = "Take";
                        break;
                    case MessageModels.ResponseTypes.Shop:
                        _buttonText.text = "Shop";
                        break;
                    case MessageModels.ResponseTypes.UpdateQuest:
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
                        ContinueGame();
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

                _msgText.text = GameManager.GameInstance.GetLevelMessage(_currentMsg.messageIds[_msgIndex]);

            }

        }

        public void ContinueGame()
        {
            SoundManager.SoundInstance.PlayEffectSound(_click);


            GameManager.GameInstance.ResumeGameplay();
        }
    }
}
