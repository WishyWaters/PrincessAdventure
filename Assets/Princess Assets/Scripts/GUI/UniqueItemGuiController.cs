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
        [SerializeField] private AudioClip _appleZinger;
        [SerializeField] private AudioClip _outfitZinger;

        [SerializeField] private Sprite _gemSprite;
        [SerializeField] private Sprite _soupSprite;
        [SerializeField] private Sprite _appleSprite;

        [SerializeField] EquipIconLookup _equipLookup;

        public void LoadUniqueItemGui(PickUps item, ItemDescription itemDescription)
        {
            //SoundManager.SoundInstance.PlayEffectSound(_powerUpZinger);

            _titleText.text = itemDescription.name;
            _descText.text = itemDescription.text;

            switch (item)
            {
                case PickUps.Staff:
                    _itemImage.sprite = _wandSprite;
                    SoundManager.SoundInstance.PlayEffectSound(_powerUpZinger);
                    break;
                case PickUps.Candle:
                    _itemImage.sprite = _candleSprite;
                    SoundManager.SoundInstance.PlayEffectSound(_powerUpZinger);
                    break;
                case PickUps.Skull:
                    _itemImage.sprite = _skullSprite;
                    SoundManager.SoundInstance.PlayEffectSound(_powerUpZinger);
                    break;
                case PickUps.Book:
                    _itemImage.sprite = _bookSprite;
                    SoundManager.SoundInstance.PlayEffectSound(_powerUpZinger);
                    break;
                case PickUps.Soup:
                    _itemImage.sprite = _soupSprite;
                    SoundManager.SoundInstance.PlayEffectSound(_powerUpZinger);
                    break;
                case PickUps.Gemstone:
                    _itemImage.sprite = _gemSprite;
                    SoundManager.SoundInstance.PlayEffectSound(_powerUpZinger);
                    break;
                case PickUps.Apple:
                    _itemImage.sprite = _appleSprite;
                    SoundManager.SoundInstance.PlayEffectSound(_appleZinger);
                    break;
            }

            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(_confirmButton);

        }

        public void LoadEquipItemGui(MajorTreasures treasureType, int id, ItemDescription itemDescription)
        {

            SoundManager.SoundInstance.PlayEffectSound(_outfitZinger);
            _titleText.text = itemDescription.name;
            _descText.text = itemDescription.text;

            EquipInformation equipInfo = new EquipInformation();

            switch (treasureType)
            {
                case MajorTreasures.Friend:
                    equipInfo = _equipLookup.GetEquipIconInfo(EquipSlots.Friend, id);
                    break;
                case MajorTreasures.Hat:
                    equipInfo = _equipLookup.GetEquipIconInfo(EquipSlots.Head, id);
                    break;
                case MajorTreasures.Necklace:
                    equipInfo = _equipLookup.GetEquipIconInfo(EquipSlots.Necklace, id);
                    break;
                case MajorTreasures.Outfit:
                    equipInfo = _equipLookup.GetEquipIconInfo(EquipSlots.Body, id);
                    break;
                case MajorTreasures.Ring:
                    equipInfo = _equipLookup.GetEquipIconInfo(EquipSlots.Ring, id);
                    break;
                case MajorTreasures.Shoes:
                    equipInfo = _equipLookup.GetEquipIconInfo(EquipSlots.Feet, id);
                    break;
            }

            _itemImage.sprite = equipInfo.icon;
            _itemImage.color = equipInfo.color;

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
