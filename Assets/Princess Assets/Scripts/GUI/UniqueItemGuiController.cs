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
        [SerializeField] private Sprite _wandSprite;
        [SerializeField] private Sprite _candleSprite;
        [SerializeField] private Sprite _skullSprite;
        [SerializeField] private Sprite _bookSprite;

        [SerializeField] private Image _itemImage;
        [SerializeField] private TextMeshProUGUI _titleText;
        [SerializeField] private TextMeshProUGUI _descText;
        [SerializeField] private GameObject _confirmButton;

        [SerializeField] private AudioClip _click;
        [SerializeField] private AudioClip _powerUpZinger;

        [SerializeField] private Sprite _gemSprite;
        [SerializeField] private Sprite _soupSprite;

        public void LoadUniqueItemGui(PickUps item)
        {
            SoundManager.SoundInstance.PlayEffectSound(_powerUpZinger);

            switch (item)
            {
                case PickUps.Staff:
                    _itemImage.sprite = _wandSprite;
                    _titleText.text = "Star Wand";
                    _descText.text = "A magical wand used to calm emotions, reflect danger, and find wishes.";
                    break;
                case PickUps.Candle:
                    _itemImage.sprite = _candleSprite;
                    _titleText.text = "Ever Candle";
                    _descText.text = "A candle that shoots a fire ball.  Do not use on birthday cakes.";
                    break;
                case PickUps.Skull:
                    _itemImage.sprite = _skullSprite;
                    _titleText.text = "Ghost Skull";
                    _descText.text = "A ghastly prize that grants the ghostly power to fade walk.";
                    break;
                case PickUps.Book:
                    _itemImage.sprite = _bookSprite;
                    _titleText.text = "Boom Book";
                    _descText.text = "An entire book on how to create magic explosions.  Becareful!";
                    break;
                case PickUps.Soup:
                    _itemImage.sprite = _soupSprite;
                    _titleText.text = "Monster Soup";
                    _descText.text = "A tasty treat for ";
                    break;
                case PickUps.Gemstone:
                    _itemImage.sprite = _gemSprite;
                    _titleText.text = "Mirror Gem";
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
