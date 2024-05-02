using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PrincessAdventure
{
    public class GamePauseGuiController : MonoBehaviour
    {
        [SerializeField] private GameObject CustomizerPanel;
        [SerializeField] private GameObject EquipmentPanel;
        [SerializeField] private GameObject QuestPanel;
        [SerializeField] private GameObject GameplayGuiPanel;

        private GameObject _lastViewedPanel = null;

        void Update()
        {
            if (Input.GetButtonUp("LeftBump"))
            {
                if (_lastViewedPanel == CustomizerPanel)
                    OpenEquipment();
                else if (_lastViewedPanel == EquipmentPanel)
                    OpenQuest();
                else
                    OpenCustomizer();
            }
            else if(Input.GetButtonUp("RightBump"))
            {
                if (_lastViewedPanel == CustomizerPanel)
                    OpenQuest();
                else if (_lastViewedPanel == EquipmentPanel)
                    OpenCustomizer();
                else
                    OpenEquipment();
            }

        }

            //Toggle panel functions
        public void InitializePause()
        {
           

            if (_lastViewedPanel == CustomizerPanel)
                OpenCustomizer();
            else if (_lastViewedPanel == QuestPanel)
                OpenQuest();
            else 
                OpenEquipment();
            
        }

        public void OpenCustomizer()
        {
            SoundManager.SoundInstance.PlayUiEventSmall();

            CustomizerPanel.SetActive(true);
            EquipmentPanel.SetActive(false);
            QuestPanel.SetActive(false);
            GameplayGuiPanel.SetActive(false);

            _lastViewedPanel = CustomizerPanel;
            //CustomizerPanel.GetComponent<CustomizeCharGuiController>().InitializeCustomizerGui();
        }

        public void OpenEquipment()
        {
            SoundManager.SoundInstance.PlayUiEventSmall();

            CustomizerPanel.SetActive(false);
            EquipmentPanel.SetActive(true);
            QuestPanel.SetActive(false);
            GameplayGuiPanel.SetActive(true);

            _lastViewedPanel = EquipmentPanel;
            EquipmentPanel.GetComponent<EquipmentGuiController>().InitializeEquipmentScreen();
        }

        public void OpenQuest()
        {
            SoundManager.SoundInstance.PlayUiEventSmall();

            CustomizerPanel.SetActive(false);
            EquipmentPanel.SetActive(false);
            QuestPanel.SetActive(true);
            GameplayGuiPanel.SetActive(false);

            _lastViewedPanel = QuestPanel;
            QuestPanel.GetComponent<QuestGuiController>().InitializeQuestPanel();
        }


    }
}
