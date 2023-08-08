using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PrincessAdventure
{

    public class EquipIconLookup : MonoBehaviour
    {
        [SerializeField] private List<EquipInformation> _hatIcons;
        [SerializeField] private List<EquipInformation> _outfitIcons;
        [SerializeField] private List<EquipInformation> _shoeIcons;
        [SerializeField] private List<EquipInformation> _necklaceIcons;
        [SerializeField] private List<EquipInformation> _ringIcons;
        [SerializeField] private List<EquipInformation> _friendIcons;

        [SerializeField] Sprite _bonusHeartSprite;
        [SerializeField] Sprite _bonusMagicSprite;
        [SerializeField] Sprite _lockedSprite;


        public EquipInformation GetEquipIconInfo(EquipSlots slotType, int id)
        {

            switch(slotType)
            {
                case EquipSlots.Head:
                    if (id < 0 || id > 9)
                        return null;
                    return _hatIcons[id - 1];
                case EquipSlots.Body:
                    if (id < 0 || id > 9)
                        return null;
                    return _outfitIcons[id - 1];
                case EquipSlots.Feet:
                    if (id < 0 || id > 9)
                        return null;
                    return _shoeIcons[id - 1];
                case EquipSlots.Necklace:
                    if (id < 0 || id > 4)
                        return null;
                    return _necklaceIcons[id - 1];
                case EquipSlots.Ring:
                    if (id < 0 || id > 4)
                        return null;
                    return _ringIcons[id - 1];
                case EquipSlots.Friend:
                    if (id < 0 || id > 3)
                        return null;
                    return _friendIcons[id - 1];
                default:
                    return null;
            }
        }

        public Sprite GetBonusHeartSprite()
        {
            return _bonusHeartSprite;
        }

        public Sprite GetBonusMagicSprite()
        {
            return _bonusMagicSprite;
        }

        public Sprite GetLockedSprite()
        {
            return _lockedSprite;
        }
    }
}