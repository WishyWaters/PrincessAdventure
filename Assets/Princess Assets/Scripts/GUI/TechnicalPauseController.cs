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