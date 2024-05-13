using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace PrincessAdventure
{
    public class TechnicalPauseController : MonoBehaviour
    {
        [SerializeField] private GameObject _continueBtn;
        [SerializeField] private GameObject _quitBtn;

        private GameObject _highlightedObject = null;

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

        public void LoadTechPause()
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(_continueBtn);
        }

        public void ContinueGame()
        {
            GameManager.GameInstance.ResumeGameplay();
        }

        public void QuitGame()
        {
            GameManager.GameInstance.LoadMainMenu();
        }

    }
}