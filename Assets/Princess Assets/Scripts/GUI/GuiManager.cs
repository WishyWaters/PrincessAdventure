using UnityEngine;
using System.Collections;

namespace PrincessAdventure
{

    public class GuiManager : MonoBehaviour
    {
        [Header("Panels")]
        [SerializeField] GameObject _gameplayPanel;
        [SerializeField] GameObject _defeatPanel;
        [SerializeField] GameObject _starShardPanel;
        [SerializeField] GameObject _powerUpPanel;
        [SerializeField] GameObject _messagePanel;
        [SerializeField] GameObject _fadePanel;
        [SerializeField] GameObject _uniqueItemPanel;
        [SerializeField] GameObject _mainMenuPanel;
        [SerializeField] GameObject _techPausePanel;
        [SerializeField] GameObject _gamePausePanel;

        [Header("Scripts")]
        [SerializeField] GameplayGuiController _gameplayGui;
        [SerializeField] DefeatGuiController _defeatGui;
        [SerializeField] StarShardGuiController _starShardGui;
        [SerializeField] PowerUpGuiController _powerUpGui;
        [SerializeField] MessageGuiController _messageGui;
        [SerializeField] ScreenFadeController _fadeGui;
        [SerializeField] UniqueItemGuiController _itemGui;
        [SerializeField] MainMenuController _mainMenu;
        [SerializeField] TechnicalPauseController _techPauseGui;
        [SerializeField] GamePauseGuiController _gamePauseGui;

        public static GuiManager GuiInstance;

        #region Unity Functions
        private void Awake()
        {
            if (GuiInstance == null)
            {
                DontDestroyOnLoad(this);
                GuiInstance = this;
            }
            else if (GuiInstance != this)
            {
                Destroy(gameObject);
            }
        }
        #endregion
        public void DeactivateAllGui()
        {
            _gameplayPanel.SetActive(false);
            _defeatPanel.SetActive(false);
            _starShardPanel.SetActive(false);
            _powerUpPanel.SetActive(false);
            _messagePanel.SetActive(false);
            _fadePanel.SetActive(false);
            _uniqueItemPanel.SetActive(false);
            _mainMenuPanel.SetActive(false);
            _techPausePanel.SetActive(false);
            _gamePausePanel.SetActive(false);
        }

        public void DeactivateAllExceptFade()
        {
            _gameplayPanel.SetActive(false);
            _defeatPanel.SetActive(false);
            _starShardPanel.SetActive(false);
            _powerUpPanel.SetActive(false);
            _messagePanel.SetActive(false);
            _uniqueItemPanel.SetActive(false);
            _mainMenuPanel.SetActive(false);
            _techPausePanel.SetActive(false);
            _gamePausePanel.SetActive(false);
        }


        public void LoadGameplayGui(ActiveGame gameDetails)
        {
            _gameplayPanel.SetActive(true);
            _defeatPanel.SetActive(false);
            _starShardPanel.SetActive(false);
            _powerUpPanel.SetActive(false);
            _messagePanel.SetActive(false);
            _fadePanel.SetActive(false);
            _uniqueItemPanel.SetActive(false);
            _mainMenuPanel.SetActive(false);
            _techPausePanel.SetActive(false);
            _gamePausePanel.SetActive(false);

            _gameplayGui.LoadGameplayGui(gameDetails);
        }

        public void UpdateGameplayGui(ActiveGame gameDetails)
        {
            _gameplayGui.LoadGameplayGui(gameDetails);
        }

        public void LoadDefeatGui()
        {
            _gameplayPanel.SetActive(false);
            _defeatPanel.SetActive(true);
            _starShardPanel.SetActive(false);
            _powerUpPanel.SetActive(false);
            _messagePanel.SetActive(false);
            _fadePanel.SetActive(false);
            _uniqueItemPanel.SetActive(false);
            _mainMenuPanel.SetActive(false);
            _techPausePanel.SetActive(false);
            _gamePausePanel.SetActive(false);

            _defeatGui.LoadDefeatScreen();
        }

        public void LoadStarShardGui(int numOfShards)
        {
            _gameplayPanel.SetActive(false);
            _defeatPanel.SetActive(false);
            _starShardPanel.SetActive(true);
            _powerUpPanel.SetActive(false);
            _messagePanel.SetActive(false);
            _fadePanel.SetActive(false);
            _uniqueItemPanel.SetActive(false);
            _mainMenuPanel.SetActive(false);
            _techPausePanel.SetActive(false);
            _gamePausePanel.SetActive(false);

            _starShardGui.LoadStarShardScreen(numOfShards);
        }

        public void LoadPowerUpGui()
        {
            _gameplayPanel.SetActive(false);
            _defeatPanel.SetActive(false);
            _starShardPanel.SetActive(false);
            _powerUpPanel.SetActive(true);
            _messagePanel.SetActive(false);
            _fadePanel.SetActive(false);
            _uniqueItemPanel.SetActive(false);
            _mainMenuPanel.SetActive(false);
            _techPausePanel.SetActive(false);
            _gamePausePanel.SetActive(false);

            _powerUpGui.LoadPowerUpScreen();
        }

        public void LoadMessageGui(string msgText)
        {
            _gameplayPanel.SetActive(false);
            _defeatPanel.SetActive(false);
            _starShardPanel.SetActive(false);
            _powerUpPanel.SetActive(false);
            _messagePanel.SetActive(true);
            _fadePanel.SetActive(false);
            _uniqueItemPanel.SetActive(false);
            _mainMenuPanel.SetActive(false);
            _techPausePanel.SetActive(false);
            _gamePausePanel.SetActive(false);

            _messageGui.LoadMessageGui(msgText);
        }

        public void LoadUniqueItemGui(PickUps item, int id=0)
        {
            _gameplayPanel.SetActive(false);
            _defeatPanel.SetActive(false);
            _starShardPanel.SetActive(false);
            _powerUpPanel.SetActive(false);
            _messagePanel.SetActive(false);
            _fadePanel.SetActive(false);
            _uniqueItemPanel.SetActive(true);
            _mainMenuPanel.SetActive(false);
            _techPausePanel.SetActive(false);
            _gamePausePanel.SetActive(false);

            _itemGui.LoadUniqueItemGui(item);
        }

        public void LoadMainMenuGui()
        {
            _gameplayPanel.SetActive(false);
            _defeatPanel.SetActive(false);
            _starShardPanel.SetActive(false);
            _powerUpPanel.SetActive(false);
            _messagePanel.SetActive(false);
            _fadePanel.SetActive(false);
            _uniqueItemPanel.SetActive(false);
            _mainMenuPanel.SetActive(true);
            _techPausePanel.SetActive(false);
            _gamePausePanel.SetActive(false);

            _mainMenu.LoadMainMenu();
        }

        public void LoadTechnicalPause()
        {
            _gameplayPanel.SetActive(false);
            _defeatPanel.SetActive(false);
            _starShardPanel.SetActive(false);
            _powerUpPanel.SetActive(false);
            _messagePanel.SetActive(false);
            _fadePanel.SetActive(false);
            _uniqueItemPanel.SetActive(false);
            _mainMenuPanel.SetActive(false);
            _techPausePanel.SetActive(true);
            _gamePausePanel.SetActive(false);

            _techPauseGui.LoadTechPause();
        }

        public void LoadGamePause()
        {
            _gameplayPanel.SetActive(false);
            _defeatPanel.SetActive(false);
            _starShardPanel.SetActive(false);
            _powerUpPanel.SetActive(false);
            _messagePanel.SetActive(false);
            _fadePanel.SetActive(false);
            _uniqueItemPanel.SetActive(false);
            _mainMenuPanel.SetActive(false);
            _techPausePanel.SetActive(false);
            _gamePausePanel.SetActive(true);

            _gamePauseGui.InitializePause();
        }

        public void EmptyOneHeart()
        {
            _gameplayGui.EmptyOneHeart();
        }

        public void HealOneHeart()
        {
            _gameplayGui.RefillOneHeart();
        }

        public void UpdateGoldText(int goldAmt)
        {
            _gameplayGui.UpdateGoldText(goldAmt);
        }

        public void UpdateKeyText(int keys)
        {
            _gameplayGui.UpdateKeyText(keys);

        }

        public void UpdateMana(int currentMana, int maxMana)
        {
            _gameplayGui.UpdateMana(currentMana, maxMana);
        }

        public void StartTimerGui(int time)
        {
            _gameplayGui.StartTimerGui(time);
        }

        public void UpdateTimerText(int time)
        {
            _gameplayGui.UpdateTimerText(time);
        }

        public void EndTimerGui()
        {
            _gameplayGui.EndTimerGui();
        }

        public void BlackOut()
        {
            _fadeGui.BlackOut();
        }

        public void ClearOut()
        {
            _fadeGui.ClearOut();
        }

        public void FillToBlack(FadeTypes type, float timeToTake)
        {
            _fadePanel.SetActive(true);
            StartCoroutine(_fadeGui.FillToBlack(type, timeToTake));
        }

        public void FillToClear(FadeTypes type, float timeToTake)
        {
            _fadePanel.SetActive(true);
            StartCoroutine(_fadeGui.FillToClear(type, timeToTake));
        }
    }
}
