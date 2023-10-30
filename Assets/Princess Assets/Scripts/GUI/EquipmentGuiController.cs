using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace PrincessAdventure
{
    public class EquipmentGuiController : MonoBehaviour
    {
        [SerializeField] GameObject _headSelected;
        [SerializeField] GameObject _bodySelected;
        [SerializeField] GameObject _feetSelected;
        [SerializeField] GameObject _neckSelected;
        [SerializeField] GameObject _ringSelected;
        [SerializeField] GameObject _friendSelected;

        [SerializeField] EquipSelectGuiController _equipSelectCtrl;

        private GameObject _highlightedObject = null;
        private EquipSlots _clickedSlot;
        // Start is called before the first frame update
        void Start()
        {

            _headSelected.GetComponent<Button>().onClick.AddListener(delegate { OpenEquipSelect(EquipSlots.Head); });
            _bodySelected.GetComponent<Button>().onClick.AddListener(delegate { OpenEquipSelect(EquipSlots.Body); });
            _feetSelected.GetComponent<Button>().onClick.AddListener(delegate { OpenEquipSelect(EquipSlots.Feet); });
            _neckSelected.GetComponent<Button>().onClick.AddListener(delegate { OpenEquipSelect(EquipSlots.Necklace); });
            _ringSelected.GetComponent<Button>().onClick.AddListener(delegate { OpenEquipSelect(EquipSlots.Ring); });
            _friendSelected.GetComponent<Button>().onClick.AddListener(delegate { OpenEquipSelect(EquipSlots.Friend); });

            InitializeEquipmentScreen();
        }

        // Update is called once per frame
        void Update()
        {
            //On Cancel Input, initialize equipment to close equip select
            if (Input.GetButtonUp("Cancel") && _equipSelectCtrl.isActiveAndEnabled)
            {
                InitializeEquipmentScreen(_clickedSlot);
                SoundManager.SoundInstance.PlayUiCancel();

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

        public void InitializeEquipmentScreen(EquipSlots highlightedSlot = EquipSlots.Head)
        {
            EventSystem.current.SetSelectedGameObject(null);
            switch(highlightedSlot)
            {
                case EquipSlots.Body:
                    EventSystem.current.SetSelectedGameObject(_bodySelected);
                    break;
                case EquipSlots.Feet:
                    EventSystem.current.SetSelectedGameObject(_feetSelected);
                    break;
                case EquipSlots.Friend:
                    EventSystem.current.SetSelectedGameObject(_friendSelected);
                    break;

                case EquipSlots.Necklace:
                    EventSystem.current.SetSelectedGameObject(_neckSelected);
                    break;
                case EquipSlots.Ring:
                    EventSystem.current.SetSelectedGameObject(_ringSelected);
                    break;
                case EquipSlots.Head:
                default:
                    EventSystem.current.SetSelectedGameObject(_headSelected);
                    break;

            }

            _equipSelectCtrl.gameObject.SetActive(false);

            //Load Icons for Current Equipment
            LoadEquipedIcons(GameManager.GameInstance.GetPrincessEquipment());

        }

        public void OpenEquipSelect(EquipSlots slot)
        {
            _clickedSlot = slot;
            _equipSelectCtrl.gameObject.SetActive(true);
            _equipSelectCtrl.InitializeEquipSelect(slot);
        }

        private void LoadEquipedIcons(PrincessEquipment equipment)
        {
            _headSelected.GetComponent<EquipIconController>().SetEquipIcon(EquipSlots.Head, equipment.selectedHat, false);
            _bodySelected.GetComponent<EquipIconController>().SetEquipIcon(EquipSlots.Body, equipment.selectedOutfit, false);
            _feetSelected.GetComponent<EquipIconController>().SetEquipIcon(EquipSlots.Feet, equipment.selectedShoes, false);
            _neckSelected.GetComponent<EquipIconController>().SetEquipIcon(EquipSlots.Necklace, equipment.selectedNecklace, false);
            _ringSelected.GetComponent<EquipIconController>().SetEquipIcon(EquipSlots.Ring, equipment.selectedRing, false);
            _friendSelected.GetComponent<EquipIconController>().SetEquipIcon(EquipSlots.Friend, equipment.selectedFriend, false);


        }
    }
}