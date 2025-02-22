using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace PrincessAdventure
{
    public class MainMenuController : MonoBehaviour
    {
        [Header("Panels")]
        [SerializeField] GameObject _mainPanel;
        [SerializeField] GameObject _savesPanel;
        [SerializeField] GameObject _settingsPanel;
        [SerializeField] GameObject _ErasePanel;
        [SerializeField] GameObject _EraseConfirmPanel;

        [Header("Main Options")]
        [SerializeField] private GameObject _continueBtn;
        [SerializeField] private GameObject _newGameBtn;
        [SerializeField] private GameObject _optionsBtn;

        [Header("Save Select")]
        [SerializeField] private GameObject _saveOneBtn;
        [SerializeField] private GameObject _saveTwoBtn;
        [SerializeField] private GameObject _saveThreeBtn;
        [SerializeField] private GameObject _backToMain;

        [Header("Game Settings")]
        [SerializeField] private GameObject _musicSlider;
        [SerializeField] private GameObject _sfxSlider;
        [SerializeField] private Toggle _tipsToggle;
        [SerializeField] private GameObject _confirmSettingsBtn;
        [SerializeField] private GameObject _cancelSettingsBtn;
        [SerializeField] private AudioClip _sfxTextClip;
        
        [Header("Delete Save Select")]
        [SerializeField] private GameObject _deleteSaveOneBtn;
        [SerializeField] private GameObject _deleteSaveTwoBtn;
        [SerializeField] private GameObject _deleteSaveThreeBtn;
        [SerializeField] private GameObject _backToLevelSelect;
        [SerializeField] private GameObject _confirmDeleteBtn;
        [SerializeField] private GameObject _cancelDeleteBtn;

        [Header("Audio")]
        [SerializeField] private AudioClip _click;
        [SerializeField] private AudioClip _eraseWarning;
        [SerializeField] private AudioClip _eraseDone;
        [SerializeField] private AudioClip _gameStart;

        private GameObject _highlightedObject = null;




        private bool _hasSave;
        private float _soundCheckCooldown;
        private int _saveToErase;

        private void Start()
        {
            CheckForRecentSave();
        }

        void Update()
        {
            //On Cancel Input, initialize equipment to close equip select
            if (Input.GetButtonUp("Cancel") )
            {
                //TODO: Back up menu
                //SoundManager.SoundInstance.PlayUiCancel();

            }



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

        public void CheckForRecentSave()
        {

            if (PlayerPrefs.HasKey("RecentSaveId"))
            {
                _hasSave = true;
                //_recentSaveId = PlayerPrefs.GetInt("RecentSaveId");
            }
            else
                _hasSave = false;


        }

        #region Loading Panels
        public void LoadMainMenu()
        {
            _mainPanel.SetActive(true);
            _savesPanel.SetActive(false);
            _settingsPanel.SetActive(false);
            _ErasePanel.SetActive(false);
            _EraseConfirmPanel.SetActive(false);

            CheckForRecentSave();

            EventSystem.current.SetSelectedGameObject(null);
            
            if (_hasSave)
            {
                _continueBtn.SetActive(true);
                EventSystem.current.SetSelectedGameObject(_continueBtn);
            }
            else
            {
                _continueBtn.SetActive(false);
                EventSystem.current.SetSelectedGameObject(_newGameBtn);
            }
        }

        public void LoadSaveSelect()
        {
            SoundManager.SoundInstance.PlayEffectSound(_click);
            _mainPanel.SetActive(false);
            _savesPanel.SetActive(true);
            _settingsPanel.SetActive(false);
            _ErasePanel.SetActive(false);
            _EraseConfirmPanel.SetActive(false);

            SaveButtonLayoutController layoutOne = _saveOneBtn.GetComponent<SaveButtonLayoutController>();
            SaveButtonLayoutController layoutTwo = _saveTwoBtn.GetComponent<SaveButtonLayoutController>();
            SaveButtonLayoutController layoutThree = _saveThreeBtn.GetComponent<SaveButtonLayoutController>();
            layoutOne.LoadButtonLayout(1);
            layoutTwo.LoadButtonLayout(2);
            layoutThree.LoadButtonLayout(3);

            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(_saveOneBtn);


        }

        public void LoadSaveEraseSelect()
        {
            SoundManager.SoundInstance.PlayEffectSound(_click);
            _mainPanel.SetActive(false);
            _savesPanel.SetActive(false);
            _settingsPanel.SetActive(false);
            _ErasePanel.SetActive(true);
            _EraseConfirmPanel.SetActive(false);

            SaveButtonLayoutController layoutOne = _deleteSaveOneBtn.GetComponent<SaveButtonLayoutController>();
            SaveButtonLayoutController layoutTwo = _deleteSaveTwoBtn.GetComponent<SaveButtonLayoutController>();
            SaveButtonLayoutController layoutThree = _deleteSaveThreeBtn.GetComponent<SaveButtonLayoutController>();
            layoutOne.LoadButtonLayout(1);
            layoutTwo.LoadButtonLayout(2);
            layoutThree.LoadButtonLayout(3);

            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(_backToLevelSelect);

        }

        public void LoadEraseConfirm()
        {
            _EraseConfirmPanel.SetActive(true);
            SoundManager.SoundInstance.PlayEffectSound(_eraseWarning);


            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(_cancelDeleteBtn);

        }

        public void QuitGame()
        {
            GameManager.GameInstance.QuitGame();
        }

        #endregion

        #region Game Saves

        public void ContinueRecentGame()
        {
            StartGame(PlayerPrefs.GetInt("RecentSaveId"));

        }

        public void ContinueSaveOne()
        {
            StartGame(1);
        }

        public void ContinueSaveTwo()
        {
            StartGame(2);
        }

        public void ContinueSaveThree()
        {
            StartGame(3);
        }

        private void StartGame(int saveId)
        {
            SoundManager.SoundInstance.PlayEffectSound(_gameStart);
            GameManager.GameInstance.StartGame(saveId);
        }

        public void CancelErase()
        {
            SoundManager.SoundInstance.PlayEffectSound(_click);
            _EraseConfirmPanel.SetActive(false);


            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(_backToLevelSelect);
        }

        public void ConfirmErase()
        {
            SoundManager.SoundInstance.PlayEffectSound(_eraseDone);

            SaveDataManager.EraseSave(_saveToErase);

            LoadSaveSelect();
        }

        public void EraseSaveOne()
        {
            _saveToErase = 1;
            LoadEraseConfirm();
        }

        public void EraseSaveTwo()
        {
            _saveToErase = 2;
            LoadEraseConfirm();
        }

        public void EraseSaveThree()
        {
            _saveToErase = 3;
            LoadEraseConfirm();
        }
        #endregion

        #region Settings

        public void LoadSettings()
        {
            SoundManager.SoundInstance.PlayEffectSound(_click);
            _soundCheckCooldown = Time.unscaledTime + 2;

            _mainPanel.SetActive(false);
            _savesPanel.SetActive(false);
            _settingsPanel.SetActive(true);
            _ErasePanel.SetActive(false);
            _EraseConfirmPanel.SetActive(false);

            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(_musicSlider);

            //Load Settings from player pref
            float music = 1f;
            if (PlayerPrefs.HasKey("MusicVolume"))
                music = PlayerPrefs.GetFloat("MusicVolume");

            float sfx = 1;
            if (PlayerPrefs.HasKey("SfxVolume"))
                sfx = PlayerPrefs.GetFloat("SfxVolume");

            bool tips = true;
            if (PlayerPrefs.HasKey("Tips"))
                tips = PlayerPrefs.GetInt("Tips") == 0 ? false : true;

            //set values
            Slider musicSlider = _musicSlider.GetComponent<Slider>();
            musicSlider.value = music;

            Slider sfxSlider = _sfxSlider.GetComponent<Slider>();
            sfxSlider.value = sfx;

            _tipsToggle.isOn = tips;
        }

        public void UpdateMusicOnChange()
        {
            Slider musicSlider = _musicSlider.GetComponent<Slider>();
            SoundManager.SoundInstance.ChangeMusicVolume(musicSlider.value);

        }

        public void UpdateSfxOnChange()
        {
            Slider sfxSlider = _sfxSlider.GetComponent<Slider>();
            SoundManager.SoundInstance.ChangeSfxVolume(sfxSlider.value);

            if (_soundCheckCooldown < Time.unscaledTime)
            {
                SoundManager.SoundInstance.PlayEffectSound(_sfxTextClip);
                _soundCheckCooldown = Time.unscaledTime + .3f;
            }
        }


        public void RestoreSound()
        {
            if (PlayerPrefs.HasKey("MusicVolume"))
            {
                float music = PlayerPrefs.GetFloat("MusicVolume");
                SoundManager.SoundInstance.ChangeMusicVolume(music);
            }

            if (PlayerPrefs.HasKey("SfxVolume"))
            {
                float sfx = PlayerPrefs.GetFloat("SfxVolume");
                SoundManager.SoundInstance.ChangeSfxVolume(sfx);
            }

        }

        public void SaveGameSettings()
        {
            SoundManager.SoundInstance.PlayEffectSound(_click);

            //set values
            Slider musicSlider = _musicSlider.GetComponent<Slider>();
            Slider sfxSlider = _sfxSlider.GetComponent<Slider>();

            PlayerPrefs.SetFloat("MusicVolume", musicSlider.value);
            PlayerPrefs.SetFloat("SfxVolume", sfxSlider.value);
            PlayerPrefs.SetInt("Tips", _tipsToggle.isOn == false ? 0 : 1);

            PlayerPrefs.Save();

            SoundManager.SoundInstance.ChangeMusicVolume(musicSlider.value);
            SoundManager.SoundInstance.ChangeSfxVolume(sfxSlider.value);
            GameManager.GameInstance.UpdateShowTips(_tipsToggle.isOn);
            LoadMainMenu();
        }

        #endregion



    }
}
