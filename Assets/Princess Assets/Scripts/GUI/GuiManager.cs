using UnityEngine;
using System.Collections;
using System.Linq;

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
        [SerializeField] GameObject _charCreatePanel;

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
        [SerializeField] CustomizeCharGuiController _custCharGui;

        [Header("Documents")]
        [SerializeField] private TextAsset _ItemText;
        [SerializeField] private TextAsset _QuestText;

        public static GuiManager GuiInstance;
        private ItemDescriptions _itemDescriptions;
        private QuestDescriptions _questDescriptions;

        #region Unity Functions
        private void Awake()
        {
            if (GuiInstance == null)
            {
                DontDestroyOnLoad(this);
                GuiInstance = this;
                LoadTextAssets();
            }
            else if (GuiInstance != this)
            {
                Destroy(gameObject);
            }
        }

        private void LoadTextAssets()
        {

            if (_ItemText != null)
                _itemDescriptions = JsonUtility.FromJson<ItemDescriptions>(_ItemText.text);

            if (_QuestText != null)
                _questDescriptions = JsonUtility.FromJson<QuestDescriptions>(_QuestText.text);

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
            _charCreatePanel.SetActive(false);
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
            _charCreatePanel.SetActive(false);
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
            _charCreatePanel.SetActive(false);

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
            _charCreatePanel.SetActive(false);

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
            _charCreatePanel.SetActive(false);

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
            _charCreatePanel.SetActive(false);

            _powerUpGui.LoadPowerUpScreen();
        }

        public void LoadMessageGui(MessageModels.Message msg, string title)
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
            _charCreatePanel.SetActive(false);

            _messageGui.LoadMessageGui(msg, title);
        }

        public void LoadUniqueItemGui(PickUps item)
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
            _charCreatePanel.SetActive(false);

            //TODO: Get item descriptions

            _itemGui.LoadUniqueItemGui(item, GetItemText(item));
        }

        public void LoadUniqueItemGui(MajorTreasures treasureType, int id)
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
            _charCreatePanel.SetActive(false);

            _itemGui.LoadEquipItemGui(treasureType, id, GetItemText(treasureType, id));
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
            _charCreatePanel.SetActive(false);

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
            _charCreatePanel.SetActive(false);

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
            _charCreatePanel.SetActive(false);

            _gamePauseGui.InitializePause();
        }

        public void LoadCharCreator()
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
            _charCreatePanel.SetActive(true);

            _custCharGui.InitializeCustomizerGui();
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

        private ItemDescription GetItemText(PickUps pickup)
        {
            switch(pickup)
            {
                case PickUps.StarShard:
                    return _itemDescriptions.uniqueItems.Where(x => x.id == 1).FirstOrDefault();
                case PickUps.Staff:
                    return _itemDescriptions.uniqueItems.Where(x => x.id == 2).FirstOrDefault();
                case PickUps.Soup:
                    return _itemDescriptions.uniqueItems.Where(x => x.id == 3).FirstOrDefault();
                case PickUps.Candle:
                    return _itemDescriptions.uniqueItems.Where(x => x.id == 4).FirstOrDefault();
                case PickUps.Skull:
                    return _itemDescriptions.uniqueItems.Where(x => x.id == 5).FirstOrDefault();
                case PickUps.Gemstone:
                    return _itemDescriptions.uniqueItems.Where(x => x.id == 6).FirstOrDefault();
                case PickUps.Book:
                    return _itemDescriptions.uniqueItems.Where(x => x.id == 7).FirstOrDefault();
                default:
                    return new ItemDescription();
            }

        }

        private ItemDescription GetItemText(MajorTreasures treasureType, int id)
        {
            switch (treasureType)
            {
                case MajorTreasures.Friend:
                    return _itemDescriptions.friends.Where(x => x.id == id).FirstOrDefault();
                case MajorTreasures.Hat:
                    return _itemDescriptions.hats.Where(x => x.id == id).FirstOrDefault();
                case MajorTreasures.Necklace:
                    return _itemDescriptions.necklaces.Where(x => x.id == id).FirstOrDefault();
                case MajorTreasures.Outfit:
                    return _itemDescriptions.outfits.Where(x => x.id == id).FirstOrDefault();
                case MajorTreasures.Ring:
                    return _itemDescriptions.rings.Where(x => x.id == id).FirstOrDefault();
                case MajorTreasures.Shoes:
                    return _itemDescriptions.shoes.Where(x => x.id == id).FirstOrDefault();
                default:
                    return new ItemDescription();
            }

        }
    }
}
