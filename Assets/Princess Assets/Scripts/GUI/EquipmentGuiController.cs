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

        // Start is called before the first frame update
        void Start()
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(_headSelected);

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

            if (_highlightedObject != EventSystem.current.currentSelectedGameObject)
            {
                if (_highlightedObject != null && _highlightedObject.GetComponent<SelectorGuiController>() != null)
                    _highlightedObject.GetComponent<SelectorGuiController>().SetHighlightCursor(false);

                _highlightedObject = EventSystem.current.currentSelectedGameObject;

                if (_highlightedObject != null && _highlightedObject.GetComponent<SelectorGuiController>() != null)
                    _highlightedObject.GetComponent<SelectorGuiController>().SetHighlightCursor(true);

                //TODO: Play sfx
            }
        }

        public void InitializeEquipmentScreen()
        {
            _equipSelectCtrl.gameObject.SetActive(false);

            //Load Icons for Current Equipment
            LoadEquipedIcons(GameManager.GameInstance.GetPrincessEquipment());

        }

        public void OpenEquipSelect(EquipSlots slot)
        {
            _equipSelectCtrl.gameObject.SetActive(true);
            _equipSelectCtrl.InitializeEquipSelect();
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