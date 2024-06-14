using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

namespace PrincessAdventure
{
    public class QuestGuiController : MonoBehaviour
    {
        [Header("Info Text")]
        [SerializeField] private Text _titleText;
        [SerializeField] private Text _descText;

        [Header("Status Info")]
        [SerializeField] private Image _starFill;
        [SerializeField] private Image _wandImage;
        [SerializeField] private Image _soupImage;
        [SerializeField] private Image _candleImage;
        [SerializeField] private Image _skullImage;
        [SerializeField] private Image _gemImage;
        [SerializeField] private Image _bookImage;
        [SerializeField] private Image _appleImage;
        [SerializeField] private Image _tradeImage;
        [SerializeField] private Text _goldText;
        [SerializeField] private Text _keyText;
        [SerializeField] private Text _appleText;

        [Header("Panel Gameobjects")]
        [SerializeField] private GameObject _starGo;
        [SerializeField] private GameObject _wandGo;
        [SerializeField] private GameObject _soupGo;
        [SerializeField] private GameObject _candleGo;
        [SerializeField] private GameObject _skullGo;
        [SerializeField] private GameObject _gemGo;
        [SerializeField] private GameObject _bookGo;
        [SerializeField] private GameObject _goldGo;
        [SerializeField] private GameObject _keyGo;
        [SerializeField] private GameObject _appleGo;
        [SerializeField] private GameObject _tradeGo;

        private GameObject _highlightedObject = null;



        private GameDetails _gameDets = null;
        private ItemDescriptions _itemDescriptions = null;

        // Start is called before the first frame update
        void Start()
        {
            //InitializeQuestPanel();
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

                SoundManager.SoundInstance.PlayUiNavigate();

                //TODO: Update Text based on what is highlighted
                LoadItemText();
            }
        }

        public void InitializeQuestPanel(GameDetails details, ItemDescriptions itemDescriptions)
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(_goldGo);

            _gameDets = details;
            _itemDescriptions = itemDescriptions;

            LoadQuestStatusGui();
        }

        private void LoadQuestStatusGui()
        {
            _starFill.fillAmount = GetFillValue(_gameDets.starShards);

            SetIconDisplay(_gameDets.hasMagic, _wandImage);
            SetIconDisplay(_gameDets.hasSummon, _soupImage);
            SetIconDisplay(_gameDets.hasFireball, _candleImage);
            SetIconDisplay(_gameDets.hasFade, _skullImage);
            SetIconDisplay(_gameDets.hasVision, _gemImage);
            SetIconDisplay(_gameDets.hasBomb, _bookImage);

            _keyText.text = _gameDets.keys.ToString();
            _goldText.text = _gameDets.gold.ToString();

            if (_gameDets.totalApples > 0)
            {
                SetIconDisplay(true, _appleImage);
                _appleText.text = _gameDets.apples.ToString();
            } else
            {
                SetIconDisplay(false, _appleImage);
                _appleText.text = "";
            }

            SetTradeIcon(_gameDets.tradeItemId, _tradeImage);
        }

        

        private void LoadItemText()
        {
            ItemDescription description = null;

            if (_highlightedObject == _starGo)
                description = GetUniqueItemText(1);
            else if (_highlightedObject == _wandGo && _gameDets.hasMagic)
                description = GetUniqueItemText(2);
            else if (_highlightedObject == _soupGo && _gameDets.hasSummon)
                description = GetUniqueItemText(3);
            else if (_highlightedObject == _candleGo && _gameDets.hasFireball)
                description = GetUniqueItemText(4);
            else if (_highlightedObject == _skullGo && _gameDets.hasFade)
                description = GetUniqueItemText(5);
            else if (_highlightedObject == _gemGo && _gameDets.hasVision)
                description = GetUniqueItemText(6);
            else if (_highlightedObject == _bookGo && _gameDets.hasBomb)
                description = GetUniqueItemText(7);
            else if (_highlightedObject == _goldGo)
                description = GetUniqueItemText(9);
            else if (_highlightedObject == _keyGo)
                description = GetUniqueItemText(10);
            else if (_highlightedObject == _appleGo && _gameDets.totalApples > 0)
                description = GetUniqueItemText(8);
            else if (_highlightedObject == _tradeGo && _gameDets.tradeItemId != 0)
                description = GetTradeItemText(_gameDets.tradeItemId);

            if(description != null)
            {
                
                _titleText.text = description.name;
                _descText.text = description.text + "\n\n" + description.extra;
            }

        }


        private float GetFillValue(int numOfShards)
        {
            if (numOfShards == 0)
                return 0f;
            if (numOfShards == 1)
                return .17f;
            if (numOfShards == 2)
                return .39f;
            if (numOfShards == 3)
                return .61f;
            if (numOfShards == 4)
                return .82f;
            if (numOfShards >= 5)
                return 1f;

            return 0;
        }

        private ItemDescription GetUniqueItemText(int itemId)
        {
            return _itemDescriptions.uniqueItems.Where(x => x.id == itemId).FirstOrDefault();

        }

        private ItemDescription GetTradeItemText(int tradeId)
        {
            return _itemDescriptions.trade.Where(x => x.id == tradeId).FirstOrDefault();

        }

        private void SetTradeIcon(int item, Image iconImage)
        {
            if(item == 0)
            {
                Color color = Color.clear;
                color.a = 0f;

                iconImage.color = color;
            }
            else
            {
                //TODO: Get Trade Icon Image
            }
        }

        private void SetIconDisplay(bool hasItem, Image iconImage)
        {
            if (!hasItem)
            {
                Color color = iconImage.color;
                color.a = 0f;

                iconImage.color = color;
            }
            else
            {
                Color color = iconImage.color;
                color.a = 1f;

                iconImage.color = color;
            }
        }
    }
}