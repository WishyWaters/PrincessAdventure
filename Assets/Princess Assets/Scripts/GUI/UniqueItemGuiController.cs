using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

namespace PrincessAdventure
{
    public class UniqueItemGuiController : MonoBehaviour
    {
        [SerializeField] private Sprite _crystalSprite;
        [SerializeField] private Sprite _candleSprite;
        [SerializeField] private Sprite _skullSprite;
        [SerializeField] private Sprite _bookSprite;

        [SerializeField] private Image _itemImage;
        [SerializeField] private TextMeshProUGUI _titleText;
        [SerializeField] private TextMeshProUGUI _descText;
        [SerializeField] private GameObject _confirmButton;

        [SerializeField] private AudioClip _click;
        [SerializeField] private AudioClip _powerUpZinger;

        public void LoadUniqueItemGui(PickUps item)
        {
            SoundManager.SoundInstance.PlayEffectSound(_powerUpZinger);

            switch (item)
            {
                case PickUps.Crystal:
                    _itemImage.sprite = _crystalSprite;
                    _titleText.text = "Mirror Crystal";
                    _descText.text = "";
                    break;
                case PickUps.Candle:
                    _itemImage.sprite = _candleSprite;
                    _titleText.text = "Ever Candle";
                    _descText.text = "";
                    break;
                case PickUps.Skull:
                    _itemImage.sprite = _skullSprite;
                    _titleText.text = "Ghost Skull";
                    _descText.text = "A ghastly prize that grants the ghostly power to fade walk.";
                    break;
                case PickUps.Book:
                    _itemImage.sprite = _bookSprite;
                    _titleText.text = "Boom Book";
                    _descText.text = "";
                    break;
            }

            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(_confirmButton);

        }

        public void ContinueGame()
        {
            SoundManager.SoundInstance.PlayEffectSound(_click);


            GameManager.GameInstance.ResumeGameplay();
        }
    }
}
