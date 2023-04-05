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

        [Header("Scripts")]
        [SerializeField] GameplayGuiController _gameplayGui;
        [SerializeField] DefeatGuiController _defeatGui;
        [SerializeField] StarShardGuiController _starShardGui;
        [SerializeField] PowerUpGuiController _powerUpGui;

        public void LoadGameplayGui(ActiveGame gameDetails)
        {
            _gameplayPanel.SetActive(true);
            _defeatPanel.SetActive(false);
            _starShardPanel.SetActive(false);
            _powerUpPanel.SetActive(false);

            _gameplayGui.LoadGameplayGui(gameDetails);
        }

        public void LoadDefeatGui()
        {
            _gameplayPanel.SetActive(false);
            _defeatPanel.SetActive(true);
            _starShardPanel.SetActive(false);
            _powerUpPanel.SetActive(false);

            _defeatGui.LoadDefeatScreen();
        }

        public void LoadStarShardGui(int numOfShards)
        {
            _gameplayPanel.SetActive(false);
            _defeatPanel.SetActive(false);
            _starShardPanel.SetActive(true);
            _powerUpPanel.SetActive(false);

            _starShardGui.LoadStarShardScreen(numOfShards);
        }

        public void LoadPowerUpGui()
        {
            _gameplayPanel.SetActive(false);
            _defeatPanel.SetActive(false);
            _starShardPanel.SetActive(false);
            _powerUpPanel.SetActive(true);

            _powerUpGui.LoadPowerUpScreen();
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


    }
}
