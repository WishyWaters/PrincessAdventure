using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace PrincessAdventure
{
    public class EquipIconController : MonoBehaviour
    {
        [SerializeField] Image _iconImage;
        [SerializeField] Image _bonusOne;
        [SerializeField] Image _bonusTwo;

        [SerializeField] EquipIconLookup _equipLookup;

        public void SetEquipIcon(EquipSlots slot, int id, bool showBonus)
        {
            EquipInformation equipInfo = GetEquipInfo(slot, id);

            if (equipInfo == null)
                return;

            _iconImage.sprite = equipInfo.icon;
            _iconImage.color = equipInfo.color;

            if (showBonus && equipInfo.bonusStats != EquipBonus.None)
            {
                _bonusOne.gameObject.SetActive(true);
                _bonusTwo.gameObject.SetActive(true);

                SetBonusIcons(equipInfo);
            }
            else
            {
                if(_bonusOne != null)
                    _bonusOne.gameObject.SetActive(false);
                if(_bonusTwo != null)
                    _bonusTwo.gameObject.SetActive(false);
            }

        }

        private EquipInformation GetEquipInfo(EquipSlots slot, int id)
        {
            if (id < 1)
                return null;
            else
                return _equipLookup.GetEquipIconInfo(slot, id);
            
        }
    

        private void SetBonusIcons(EquipInformation equipInfo)
        {
            switch (equipInfo.bonusStats)
            {
                case EquipBonus.Hearts:
                    _bonusOne.sprite = _equipLookup.GetBonusHeartSprite();
                    _bonusTwo.sprite = _equipLookup.GetBonusHeartSprite();
                    break;
                case EquipBonus.Magic:
                    _bonusOne.sprite = _equipLookup.GetBonusMagicSprite();
                    _bonusTwo.sprite = _equipLookup.GetBonusMagicSprite();
                    break;
                case EquipBonus.Both:
                    _bonusOne.sprite = _equipLookup.GetBonusHeartSprite();
                    _bonusTwo.sprite = _equipLookup.GetBonusMagicSprite();
                    break;

            }

        }
    }
}