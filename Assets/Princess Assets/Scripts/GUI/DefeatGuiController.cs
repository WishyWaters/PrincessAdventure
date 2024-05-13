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

        private GameObject _highlightedObject = null;

        private void Start()
        {
        }

        void Update()
        {

            if (_highlightedObject != EventSystem.current.currentSelectedGameObject)
            {
                if (_highlightedObject != null && _highlightedObject.GetComponent<SelectorGuiController>() != null)
                    _highlightedObject.GetComponent<SelectorGuiController>().SetHighlightCursor(false);

                _highlightedObject = EventSystem.current.currentSelectedGameObject;

                if (_highlightedObject != null && _highlightedObject.GetComponent<SelectorGuiController>() != null)
                    _highlightedObject.GetComponent<SelectorGuiController>().SetHighlightCursor(true);

                SoundManager.SoundInstance.PlayUiNavigate();

            }
        }

        public void LoadDefeatScreen()
        {
            SoundManager.SoundInstance.ChangeMusic(_defeatZinger, false);

            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(_continueButton);
            _wishText.text = GetDefeatMsg();
        }

        public string GetDefeatMsg()
        {
            int randoId = Random.Range(1, 6);

            return GameManager.GameInstance.GetLevelMessage(randoId);
        }

        public void ContinueGame()
        {
            SoundManager.SoundInstance.PlayEffectSound(_click);

            GameManager.GameInstance.ContinueGame();
        }

        public void QuitGame()
        {
            SoundManager.SoundInstance.PlayEffectSound(_click);

            GameManager.GameInstance.LoadMainMenu();
        }

        

    }
}