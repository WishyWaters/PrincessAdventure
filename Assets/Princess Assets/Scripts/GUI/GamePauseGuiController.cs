using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PrincessAdventure
{
    public class GamePauseGuiController : MonoBehaviour
    {
        [SerializeField] private GameObject MapPanel;
        [SerializeField] private GameObject EquipmentPanel;
        [SerializeField] private GameObject QuestPanel;
        [SerializeField] private GameObject GameplayGuiPanel;

        private GameObject _lastViewedPanel = null;
        private GameDetails _lastGameDets = null;
        private ItemDescriptions _lastItemDesc = null;

        //Toggle panel functions
        public void InitializePause(GameDetails details, ItemDescriptions itemDescriptions)
        {
            _lastGameDets = details;
            _lastItemDesc = itemDescriptions;

            if (_lastViewedPanel == MapPanel)
                OpenMap();
            else if (_lastViewedPanel == QuestPanel)
                OpenQuest();
            else 
                OpenEquipment();
            
        }

        public void LeftNavigation()
        {
            if (_lastViewedPanel == MapPanel)
                OpenEquipment();
            else if (_lastViewedPanel == EquipmentPanel)
                OpenQuest();
            else
                OpenMap();
        }

        public void RightNavigation()
        {
            if (_lastViewedPanel == MapPanel)
                OpenQuest();
            else if (_lastViewedPanel == EquipmentPanel)
                OpenMap();
            else
                OpenEquipment();
        }

        public void OpenMap()
        {
            SoundManager.SoundInstance.PlayUiEventSmall();

            MapPanel.SetActive(true);
            EquipmentPanel.SetActive(false);
            QuestPanel.SetActive(false);
            GameplayGuiPanel.SetActive(false);

            _lastViewedPanel = MapPanel;
            MapPanel.GetComponent<MapGuiController>().InitializeMapPanel();
        }

        public void OpenEquipment()
        {
            SoundManager.SoundInstance.PlayUiEventSmall();

            MapPanel.SetActive(false);
            EquipmentPanel.SetActive(true);
            QuestPanel.SetActive(false);
            GameplayGuiPanel.SetActive(true);

            _lastViewedPanel = EquipmentPanel;
            EquipmentPanel.GetComponent<EquipmentGuiController>().InitializeEquipmentScreen();
        }

        public void OpenQuest()
        {
            SoundManager.SoundInstance.PlayUiEventSmall();

            MapPanel.SetActive(false);
            EquipmentPanel.SetActive(false);
            QuestPanel.SetActive(true);
            GameplayGuiPanel.SetActive(false);

            _lastViewedPanel = QuestPanel;
            QuestPanel.GetComponent<QuestGuiController>().InitializeQuestPanel(_lastGameDets, _lastItemDesc);
        }


    }
}
