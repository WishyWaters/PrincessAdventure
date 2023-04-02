using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

namespace PrincessAdventure
{

    public class DefeatGuiController : MonoBehaviour
    {
        [SerializeField] private GameObject _continueButton;
        [SerializeField] private GameObject _quitButton;
        [SerializeField] private TextMeshProUGUI _wishText;
        [SerializeField] private AudioClip _click;
        [SerializeField] private AudioClip _defeatZinger;

        private void Start()
        {
        }

        public void LoadDefeatScreen()
        {
            SoundManager.SoundInstance.ChangeMusic(_defeatZinger, false);

            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(_continueButton);
            _wishText.text = "Don't get bombed out!";
        }
        public void ContinueGame()
        {
            SoundManager.SoundInstance.PlayEffectSound(_click);

            GameManager.GameInstance.ContinueGameAfterDeath();
        }

        public void QuitGame()
        {
            SoundManager.SoundInstance.PlayEffectSound(_click);

            GameManager.GameInstance.QuitGame();
        }

        //TODO:  Look up messages by death

    }
}