using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

namespace PrincessAdventure
{
    public class GameplayGuiController : MonoBehaviour
    {
        [SerializeField] private GameObject _magicProgressBar;
        [SerializeField] private GameObject _heartLayoutGroup;
        [SerializeField] private GameObject _heartContainerPrefab;
        [SerializeField] private Sprite _fullHeartSprite;
        [SerializeField] private Sprite _emptyHeartSprite;
        [SerializeField] private GameObject _magicConsumeBar;
        [SerializeField] private Text _goldText;
        [SerializeField] private Text _keyText;

        private List<GameObject> _hearts = new List<GameObject>();
        private int _currentFullHeart = 0;

        private float _magicWidthPerMagicPoint = 60f;

        // Use this for initialization
        void Start()
        {
        }

        public void LoadGameplayGui(ActiveGame gameDetails)
        {
            //Debug.Log("Loading Gameplay UI");

            LoadHearts(gameDetails.heartPoints);
            LoadMagic(gameDetails.magicPoints);
            UpdateGoldText(gameDetails.gold);
            UpdateKeyText(gameDetails.keys);
        }

        public void RefillOneHeart()
        {
            if(_currentFullHeart + 1 < _hearts.Count)
            {
                GameObject heartContainer = _hearts[_currentFullHeart + 1];
                heartContainer.GetComponent<Image>().sprite = _fullHeartSprite;
                _currentFullHeart++;
            }
        }

        public void EmptyOneHeart()
        {
            if(_currentFullHeart > 0)
            {
                GameObject heartContainer = _hearts[_currentFullHeart];
                heartContainer.GetComponent<Image>().sprite = _emptyHeartSprite;
                _currentFullHeart--;
            }

        }

        public void UpdateGoldText(int goldAmt)
        {
            _goldText.text = goldAmt.ToString();
        }

        public void UpdateKeyText(int keys)
        {
            _keyText.text = keys.ToString();
        }

        public void UpdateMana(int currentMana, int maxMana)
        {
            //fill slider
            Slider magicSlider = _magicProgressBar.GetComponent<Slider>();
            float newValue = (float)currentMana / maxMana;

            //set chaser target, if mana is decreasing
            if(newValue < magicSlider.value)
                StartCoroutine(UpdateSliderOnDelay(newValue));

            magicSlider.value = newValue;
        }

        private void LoadHearts(int numOfHearts)
        {
            GlobalUtils.DestroyChildren(_heartLayoutGroup);

            _currentFullHeart = numOfHearts - 1;

            //foreach heart instantiate a heart
            for(int i = 0; i < numOfHearts; i++)
            {
                GameObject heartContainer = Instantiate(_heartContainerPrefab, _heartLayoutGroup.transform);
                heartContainer.GetComponent<Image>().sprite = _fullHeartSprite;

                _hearts.Add(heartContainer);
            }
        }

        private void LoadMagic(int numOfMagic)
        {
            float newWidth = numOfMagic * _magicWidthPerMagicPoint;

            //set bar length
            RectTransform magicRt = _magicProgressBar.GetComponent<RectTransform>();
            magicRt.sizeDelta = new Vector2(newWidth, magicRt.sizeDelta.y);

            //fill slider
            Slider magicSlider = _magicProgressBar.GetComponent<Slider>();
            magicSlider.value = 1;

            //update chaser to match
            RectTransform chaserRt = _magicConsumeBar.GetComponent<RectTransform>();
            chaserRt.sizeDelta = new Vector2(newWidth, chaserRt.sizeDelta.y);

            Slider chaserSlider = _magicConsumeBar.GetComponent<Slider>();
            chaserSlider.value = 1;
        }

        IEnumerator UpdateSliderOnDelay(float value)
        {
            float animationWaitTime = .6f;
            float timeTicker = 0f;
            Slider chaserSlider = _magicConsumeBar.GetComponent<Slider>();

            while (timeTicker < animationWaitTime)
            {
                timeTicker += Time.deltaTime;
                yield return null;
            }
            chaserSlider.value = value;

            
        }
    }
}
