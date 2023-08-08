using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace PrincessAdventure
{
    public class PowerUpGuiController : MonoBehaviour
    {
        [SerializeField] private GameObject _heartSelectBtn;
        [SerializeField] private GameObject _magicSelectBtn;
        [SerializeField] private GameObject _confirmButton;
        [SerializeField] private GameObject _backButton;

        [SerializeField] private Image _heartImage;
        [SerializeField] private Image _magicImage;

        [SerializeField] private AudioClip _click;
        [SerializeField] private AudioClip _powerUpZinger;

        private PowerUpOptions powerSelection;

        public void LoadPowerUpScreen()
        {
            //SoundManager.SoundInstance.PlayEffectSound(_rewardZinger);

            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(_heartSelectBtn);

            
        }

        public void SelectHeart()
        {
            SoundManager.SoundInstance.PlayEffectSound(_click);

            powerSelection = PowerUpOptions.Heart;
            _heartImage.rectTransform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
            _magicImage.rectTransform.localScale = new Vector3(1f, 1f, 1f);

            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(_confirmButton);
        }

        public void SelectMagic()
        {
            SoundManager.SoundInstance.PlayEffectSound(_click);

            powerSelection = PowerUpOptions.Magic;
            _heartImage.rectTransform.localScale = new Vector3(1f, 1f, 1f);
            _magicImage.rectTransform.localScale = new Vector3(1.5f, 1.5f, 1.5f);

            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(_confirmButton);
        }


        public void Confirm()
        {
            SoundManager.SoundInstance.PlayEffectSound(_powerUpZinger);

            GameManager.GameInstance.PowerUpPrincess(powerSelection);
        }

        public void Back()
        {
            SoundManager.SoundInstance.PlayEffectSound(_click);

            EventSystem.current.SetSelectedGameObject(null);

            _heartImage.rectTransform.localScale = new Vector3(1f, 1f, 1f);
            _magicImage.rectTransform.localScale = new Vector3(1f, 1f, 1f);

            switch (powerSelection)
            {
                case PowerUpOptions.Heart:
                    EventSystem.current.SetSelectedGameObject(_heartSelectBtn);
                    break;
                case PowerUpOptions.Magic:
                    EventSystem.current.SetSelectedGameObject(_magicSelectBtn);
                    break;
            }
        }
    }
}