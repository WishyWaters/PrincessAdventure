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

        public void LoadMessageGui(string msgText)
        {
            _msgText.text = msgText;

            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(_continueButton);

        }

        public void ContinueGame()
        {
            SoundManager.SoundInstance.PlayEffectSound(_click);


            GameManager.GameInstance.ResumeGameplay();
        }
    }
}
