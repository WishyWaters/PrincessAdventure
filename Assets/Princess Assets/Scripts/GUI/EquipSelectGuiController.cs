using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace PrincessAdventure
{

    public class EquipSelectGuiController : MonoBehaviour
    {
        [SerializeField] private GameObject _option1;
        [SerializeField] private GameObject _option2;
        [SerializeField] private GameObject _option3;
        [SerializeField] private GameObject _option4;
        [SerializeField] private GameObject _option5;
        [SerializeField] private GameObject _option6;
        [SerializeField] private GameObject _option7;
        [SerializeField] private GameObject _option8;

        [SerializeField] private EquipmentGuiController _equipGuiCtrl;

        public void InitializeEquipSelect(EquipSlots slot)
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(_option1);


            PrincessEquipment equipment = GameManager.GameInstance.GetPrincessEquipment();
            ClearButtonListeners();

            switch (slot)
            {
                case EquipSlots.Head:
                    LoadHats(equipment);
                    break;
                case EquipSlots.Body:
                    LoadOutfits(equipment);
                    break;
                case EquipSlots.Feet:
                    LoadShoes(equipment);
                    break;
                case EquipSlots.Necklace:
                    LoadNecklaces(equipment);
                    break;
                case EquipSlots.Ring:
                    LoadRings(equipment);
                    break;
                case EquipSlots.Friend:
                    LoadFriends(equipment);
                    break;
            }


        }

        private void LoadHats(PrincessEquipment equipment)
        {
            _option1.SetActive(true);
            if (equipment.hats.Contains(9))
            {
                _option1.GetComponent<EquipIconController>().SetEquipIcon(EquipSlots.Head, 9, true);
                _option1.GetComponent<Button>().onClick.AddListener(delegate { UpdateSelectedEquipment(EquipSlots.Head, 9); });
            }
            else
            {
                _option1.GetComponent<EquipIconController>().SetEquipIcon(EquipSlots.Head, 1, true);
                _option1.GetComponent<Button>().onClick.AddListener(delegate { UpdateSelectedEquipment(EquipSlots.Head, 1); });
            }

            _option2.SetActive(true);
            if (equipment.hats.Contains(2))
            {
                _option2.GetComponent<EquipIconController>().SetEquipIcon(EquipSlots.Head, 2, true);
                _option2.GetComponent<Button>().onClick.AddListener(delegate { UpdateSelectedEquipment(EquipSlots.Head, 2); });
            }
            else
                _option2.GetComponent<EquipIconController>().SetEquipIcon(EquipSlots.Head, 2, false, true);

            _option3.SetActive(true);
            if (equipment.hats.Contains(3))
            {
                _option3.GetComponent<EquipIconController>().SetEquipIcon(EquipSlots.Head, 3, true);
                _option3.GetComponent<Button>().onClick.AddListener(delegate { UpdateSelectedEquipment(EquipSlots.Head, 3); });
            }
            else
                _option3.GetComponent<EquipIconController>().SetEquipIcon(EquipSlots.Head, 3, false, true);

            _option4.SetActive(true);
            if (equipment.hats.Contains(4))
            {
                _option4.GetComponent<EquipIconController>().SetEquipIcon(EquipSlots.Head, 4, true);
                _option4.GetComponent<Button>().onClick.AddListener(delegate { UpdateSelectedEquipment(EquipSlots.Head, 4); });
            }
            else
                _option4.GetComponent<EquipIconController>().SetEquipIcon(EquipSlots.Head, 4, false, true);

            _option5.SetActive(true);
            if (equipment.hats.Contains(5))
            {
                _option5.GetComponent<EquipIconController>().SetEquipIcon(EquipSlots.Head, 5, true);
                _option5.GetComponent<Button>().onClick.AddListener(delegate { UpdateSelectedEquipment(EquipSlots.Head, 5); });
            }
            else
                _option5.GetComponent<EquipIconController>().SetEquipIcon(EquipSlots.Head, 5, false, true);

            _option6.SetActive(true);
            if (equipment.hats.Contains(6))
            {
                _option6.GetComponent<EquipIconController>().SetEquipIcon(EquipSlots.Head, 6, true);
                _option6.GetComponent<Button>().onClick.AddListener(delegate { UpdateSelectedEquipment(EquipSlots.Head,6); });
            }
            else
                _option6.GetComponent<EquipIconController>().SetEquipIcon(EquipSlots.Head, 6, false, true);

            _option7.SetActive(true);
            if (equipment.hats.Contains(7))
            {
                _option7.GetComponent<EquipIconController>().SetEquipIcon(EquipSlots.Head, 7, true);
                _option7.GetComponent<Button>().onClick.AddListener(delegate { UpdateSelectedEquipment(EquipSlots.Head, 7); });
            }
            else
                _option7.GetComponent<EquipIconController>().SetEquipIcon(EquipSlots.Head, 7, false, true);

            _option8.SetActive(true);
            if (equipment.hats.Contains(8))
            {
                _option8.GetComponent<EquipIconController>().SetEquipIcon(EquipSlots.Head, 8, true);
                _option8.GetComponent<Button>().onClick.AddListener(delegate { UpdateSelectedEquipment(EquipSlots.Head, 8); });
            }
            else
                _option8.GetComponent<EquipIconController>().SetEquipIcon(EquipSlots.Head, 8, false, true);


        }

        private void LoadOutfits(PrincessEquipment equipment)
        {
            _option1.SetActive(true);
            if (equipment.outfits.Contains(9))
            {
                _option1.GetComponent<EquipIconController>().SetEquipIcon(EquipSlots.Body, 9, true);
                _option1.GetComponent<Button>().onClick.AddListener(delegate { UpdateSelectedEquipment(EquipSlots.Body, 9); });

            }
            else
            {
                _option1.GetComponent<EquipIconController>().SetEquipIcon(EquipSlots.Body, 1, true);
                _option1.GetComponent<Button>().onClick.AddListener(delegate { UpdateSelectedEquipment(EquipSlots.Body, 1); });
            }

            _option2.SetActive(true);
            if (equipment.outfits.Contains(2))
            {
                _option2.GetComponent<EquipIconController>().SetEquipIcon(EquipSlots.Body, 2, true);
                _option2.GetComponent<Button>().onClick.AddListener(delegate { UpdateSelectedEquipment(EquipSlots.Body, 2); });
            }
            else
                _option2.GetComponent<EquipIconController>().SetEquipIcon(EquipSlots.Body, 2, false, true);

            _option3.SetActive(true);
            if (equipment.outfits.Contains(3))
            {
                _option3.GetComponent<EquipIconController>().SetEquipIcon(EquipSlots.Body, 3, true);
                _option3.GetComponent<Button>().onClick.AddListener(delegate { UpdateSelectedEquipment(EquipSlots.Body, 3); });
            }
            else
                _option3.GetComponent<EquipIconController>().SetEquipIcon(EquipSlots.Body, 3, false, true);

            _option4.SetActive(true);
            if (equipment.outfits.Contains(4))
            {
                _option4.GetComponent<EquipIconController>().SetEquipIcon(EquipSlots.Body, 4, true);
                _option4.GetComponent<Button>().onClick.AddListener(delegate { UpdateSelectedEquipment(EquipSlots.Body, 4); });
            }
            else
                _option4.GetComponent<EquipIconController>().SetEquipIcon(EquipSlots.Body, 4, false, true);

            _option5.SetActive(true);
            if (equipment.outfits.Contains(5))
            {
                _option5.GetComponent<EquipIconController>().SetEquipIcon(EquipSlots.Body, 5, true);
                _option5.GetComponent<Button>().onClick.AddListener(delegate { UpdateSelectedEquipment(EquipSlots.Body, 5); });
            }
            else
                _option5.GetComponent<EquipIconController>().SetEquipIcon(EquipSlots.Body, 5, false, true);

            _option6.SetActive(true);
            if (equipment.outfits.Contains(6))
            {
                _option6.GetComponent<EquipIconController>().SetEquipIcon(EquipSlots.Body, 6, true);
                _option6.GetComponent<Button>().onClick.AddListener(delegate { UpdateSelectedEquipment(EquipSlots.Body, 6); });
            }
            else
                _option6.GetComponent<EquipIconController>().SetEquipIcon(EquipSlots.Body, 6, false, true);

            _option7.SetActive(true);
            if (equipment.outfits.Contains(7))
            {
                _option7.GetComponent<EquipIconController>().SetEquipIcon(EquipSlots.Body, 7, true);
                _option7.GetComponent<Button>().onClick.AddListener(delegate { UpdateSelectedEquipment(EquipSlots.Body, 7); });
            }
            else
                _option7.GetComponent<EquipIconController>().SetEquipIcon(EquipSlots.Body, 7, false, true);

            _option8.SetActive(true);
            if (equipment.outfits.Contains(8))
            {
                _option8.GetComponent<EquipIconController>().SetEquipIcon(EquipSlots.Body, 8, true);
                _option8.GetComponent<Button>().onClick.AddListener(delegate { UpdateSelectedEquipment(EquipSlots.Body, 8); });
            }
            else
                _option8.GetComponent<EquipIconController>().SetEquipIcon(EquipSlots.Body, 8, false, true);

        }

        private void LoadShoes(PrincessEquipment equipment)
        {
            _option1.SetActive(true);
            if (equipment.shoes.Contains(9))
            {
                _option1.GetComponent<EquipIconController>().SetEquipIcon(EquipSlots.Feet, 9, true);
                _option1.GetComponent<Button>().onClick.AddListener(delegate { UpdateSelectedEquipment(EquipSlots.Feet, 9); });

            }
            else
            {
                _option1.GetComponent<EquipIconController>().SetEquipIcon(EquipSlots.Feet, 1, true);
                _option1.GetComponent<Button>().onClick.AddListener(delegate { UpdateSelectedEquipment(EquipSlots.Feet, 1); });

            }

            _option2.SetActive(true);
            if (equipment.shoes.Contains(2))
            {
                _option2.GetComponent<EquipIconController>().SetEquipIcon(EquipSlots.Feet, 2, true);
                _option2.GetComponent<Button>().onClick.AddListener(delegate { UpdateSelectedEquipment(EquipSlots.Feet, 2); });
            }
            else
                _option2.GetComponent<EquipIconController>().SetEquipIcon(EquipSlots.Feet, 2, false, true);

            _option3.SetActive(true);
            if (equipment.shoes.Contains(3))
            {
                _option3.GetComponent<EquipIconController>().SetEquipIcon(EquipSlots.Feet, 3, true);
                _option3.GetComponent<Button>().onClick.AddListener(delegate { UpdateSelectedEquipment(EquipSlots.Feet, 3); });
            }
            else
                _option3.GetComponent<EquipIconController>().SetEquipIcon(EquipSlots.Feet, 3, false, true);

            _option4.SetActive(true);
            if (equipment.shoes.Contains(4))
            {
                _option4.GetComponent<EquipIconController>().SetEquipIcon(EquipSlots.Feet, 4, true);
                _option4.GetComponent<Button>().onClick.AddListener(delegate { UpdateSelectedEquipment(EquipSlots.Feet, 4); });
            }
            else
                _option4.GetComponent<EquipIconController>().SetEquipIcon(EquipSlots.Feet, 4, false, true);

            _option5.SetActive(true);
            if (equipment.shoes.Contains(5))
            {
                _option5.GetComponent<EquipIconController>().SetEquipIcon(EquipSlots.Feet, 5, true);
                _option5.GetComponent<Button>().onClick.AddListener(delegate { UpdateSelectedEquipment(EquipSlots.Feet, 5); });
            }
            else
                _option5.GetComponent<EquipIconController>().SetEquipIcon(EquipSlots.Feet, 5, false, true);

            _option6.SetActive(true);
            if (equipment.shoes.Contains(6))
            {
                _option6.GetComponent<EquipIconController>().SetEquipIcon(EquipSlots.Feet, 6, true);
                _option6.GetComponent<Button>().onClick.AddListener(delegate { UpdateSelectedEquipment(EquipSlots.Feet, 6); });
            }
            else
                _option6.GetComponent<EquipIconController>().SetEquipIcon(EquipSlots.Feet, 6, false, true);

            _option7.SetActive(true);
            if (equipment.shoes.Contains(7))
            {
                _option7.GetComponent<EquipIconController>().SetEquipIcon(EquipSlots.Feet, 7, true);
                _option7.GetComponent<Button>().onClick.AddListener(delegate { UpdateSelectedEquipment(EquipSlots.Feet, 7); });
            }
            else
                _option7.GetComponent<EquipIconController>().SetEquipIcon(EquipSlots.Feet, 7, false, true);

            _option8.SetActive(true);
            if (equipment.shoes.Contains(8))
            {
                _option8.GetComponent<EquipIconController>().SetEquipIcon(EquipSlots.Feet, 8, true);
                _option8.GetComponent<Button>().onClick.AddListener(delegate { UpdateSelectedEquipment(EquipSlots.Feet, 8); });
            }
            else
                _option8.GetComponent<EquipIconController>().SetEquipIcon(EquipSlots.Feet, 8, false, true);

        }

        private void LoadNecklaces(PrincessEquipment equipment)
        {
            _option1.SetActive(true);
            if (equipment.necklaces.Contains(1))
            {
                _option1.GetComponent<EquipIconController>().SetEquipIcon(EquipSlots.Necklace, 1, true);
                _option1.GetComponent<Button>().onClick.AddListener(delegate { UpdateSelectedEquipment(EquipSlots.Necklace, 1); });
            }
            else
                _option1.GetComponent<EquipIconController>().SetEquipIcon(EquipSlots.Necklace, 1, false, true);

            _option2.SetActive(true);
            if (equipment.necklaces.Contains(2))
            {
                _option2.GetComponent<EquipIconController>().SetEquipIcon(EquipSlots.Necklace, 2, true);
                _option2.GetComponent<Button>().onClick.AddListener(delegate { UpdateSelectedEquipment(EquipSlots.Necklace, 2); });
            }
            else
                _option2.GetComponent<EquipIconController>().SetEquipIcon(EquipSlots.Necklace, 2, false, true);

            _option3.SetActive(true);
            if (equipment.necklaces.Contains(3))
            {
                _option3.GetComponent<EquipIconController>().SetEquipIcon(EquipSlots.Necklace, 3, true);
                _option3.GetComponent<Button>().onClick.AddListener(delegate { UpdateSelectedEquipment(EquipSlots.Necklace, 3); });
            }
            else
                _option3.GetComponent<EquipIconController>().SetEquipIcon(EquipSlots.Necklace, 3, false, true);

            _option4.SetActive(true);
            if (equipment.necklaces.Contains(4))
            {
                _option4.GetComponent<EquipIconController>().SetEquipIcon(EquipSlots.Necklace, 4, true);
                _option4.GetComponent<Button>().onClick.AddListener(delegate { UpdateSelectedEquipment(EquipSlots.Necklace, 4); });
            }
            else
                _option4.GetComponent<EquipIconController>().SetEquipIcon(EquipSlots.Necklace, 4, false, true);

            _option5.SetActive(false);
            _option6.SetActive(false);
            _option7.SetActive(false);
            _option8.SetActive(false);
        }

        private void LoadRings(PrincessEquipment equipment)
        {
            _option1.SetActive(true);
            if (equipment.rings.Contains(1))
            {
                _option1.GetComponent<EquipIconController>().SetEquipIcon(EquipSlots.Ring, 1, true);
                _option1.GetComponent<Button>().onClick.AddListener(delegate { UpdateSelectedEquipment(EquipSlots.Ring, 1); });
            }
            else
                _option1.GetComponent<EquipIconController>().SetEquipIcon(EquipSlots.Ring, 1, false, true);

            _option2.SetActive(true);
            if (equipment.rings.Contains(2))
            {
                _option2.GetComponent<EquipIconController>().SetEquipIcon(EquipSlots.Ring, 2, true);
                _option2.GetComponent<Button>().onClick.AddListener(delegate { UpdateSelectedEquipment(EquipSlots.Ring, 2); });
            }
            else
                _option2.GetComponent<EquipIconController>().SetEquipIcon(EquipSlots.Ring, 2, false, true);

            _option3.SetActive(true);
            if (equipment.rings.Contains(3))
            {
                _option3.GetComponent<EquipIconController>().SetEquipIcon(EquipSlots.Ring, 3, true);
                _option3.GetComponent<Button>().onClick.AddListener(delegate { UpdateSelectedEquipment(EquipSlots.Ring, 3); });
            }
            else
                _option3.GetComponent<EquipIconController>().SetEquipIcon(EquipSlots.Ring, 3, false, true);

            _option4.SetActive(true);
            if (equipment.rings.Contains(4))
            {
                _option4.GetComponent<EquipIconController>().SetEquipIcon(EquipSlots.Ring, 4, true);
                _option4.GetComponent<Button>().onClick.AddListener(delegate { UpdateSelectedEquipment(EquipSlots.Ring, 4); });
            }
            else
                _option4.GetComponent<EquipIconController>().SetEquipIcon(EquipSlots.Ring, 4, false, true);

            _option5.SetActive(false);
            _option6.SetActive(false);
            _option7.SetActive(false);
            _option8.SetActive(false);
        }

        private void LoadFriends(PrincessEquipment equipment)
        {
            _option1.SetActive(true);
            if (equipment.friends.Contains(1))
            {
                _option1.GetComponent<EquipIconController>().SetEquipIcon(EquipSlots.Friend, 1, true);
                _option1.GetComponent<Button>().onClick.AddListener(delegate { UpdateSelectedEquipment(EquipSlots.Friend, 1); });
            }
            else
                _option1.GetComponent<EquipIconController>().SetEquipIcon(EquipSlots.Friend, 1, false, true);

            _option2.SetActive(true);
            if (equipment.friends.Contains(2))
            {
                _option2.GetComponent<EquipIconController>().SetEquipIcon(EquipSlots.Friend, 2, true);
                _option2.GetComponent<Button>().onClick.AddListener(delegate { UpdateSelectedEquipment(EquipSlots.Friend, 2); });
            }
            else
                _option2.GetComponent<EquipIconController>().SetEquipIcon(EquipSlots.Friend, 2, false, true);

            _option3.SetActive(true);
            if (equipment.friends.Contains(3))
            {
                _option3.GetComponent<EquipIconController>().SetEquipIcon(EquipSlots.Friend, 3, true);
                _option3.GetComponent<Button>().onClick.AddListener(delegate { UpdateSelectedEquipment(EquipSlots.Friend, 3); });
            }
            else
                _option3.GetComponent<EquipIconController>().SetEquipIcon(EquipSlots.Friend, 3, false, true);

            _option4.SetActive(false);
            _option5.SetActive(false);
            _option6.SetActive(false);
            _option7.SetActive(false);
            _option8.SetActive(false);
        }

        private void ClearButtonListeners()
        {
            _option1.GetComponent<Button>().onClick.RemoveAllListeners();
            _option2.GetComponent<Button>().onClick.RemoveAllListeners();
            _option3.GetComponent<Button>().onClick.RemoveAllListeners();
            _option4.GetComponent<Button>().onClick.RemoveAllListeners();
            _option5.GetComponent<Button>().onClick.RemoveAllListeners();
            _option6.GetComponent<Button>().onClick.RemoveAllListeners();
            _option7.GetComponent<Button>().onClick.RemoveAllListeners();
            _option8.GetComponent<Button>().onClick.RemoveAllListeners();
        }



        public void UpdateSelectedEquipment(EquipSlots slot, int id)
        {
            PrincessEquipment equipment = GameManager.GameInstance.GetPrincessEquipment();

            switch(slot)
            {
                case EquipSlots.Head:
                    equipment.selectedHat = id;
                    break;
                case EquipSlots.Body:
                    equipment.selectedOutfit = id;
                    break;
                case EquipSlots.Feet:
                    equipment.selectedShoes = id;
                    break;
                case EquipSlots.Necklace:
                    equipment.selectedNecklace = id;
                    break;
                case EquipSlots.Ring:
                    equipment.selectedRing = id;
                    break;
                case EquipSlots.Friend:
                    equipment.selectedFriend = id;
                    break;
            }

            GameManager.GameInstance.SetPrincessEquipment(equipment);

            _equipGuiCtrl.InitializeEquipmentScreen();
        }
    }
}