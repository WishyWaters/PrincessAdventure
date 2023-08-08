using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

namespace PrincessAdventure
{
    public class SaveButtonLayoutController : MonoBehaviour
    {
        [SerializeField] private GameObject _heartLayoutGroup;
        [SerializeField] private GameObject _heartImage;
        [SerializeField] private GameObject _magicBar;
        [SerializeField] private GameObject _goldImage;
        [SerializeField] private GameObject _keyImage;
        [SerializeField] private TextMeshProUGUI _levelText;
        [SerializeField] private TextMeshProUGUI _goldText;
        [SerializeField] private TextMeshProUGUI _keyText;
        [SerializeField] private Text _completedText;
        [SerializeField] private bool _isEraseButton;

        public void LoadButtonLayout(int saveId)
        {
            GameDetails game = SaveDataManager.LoadGameDetails(saveId);

            if(game == null)
            {
                if(_isEraseButton)
                    this.gameObject.SetActive(false);

                _heartLayoutGroup.SetActive(false);
                _magicBar.SetActive(false);
                _goldImage.SetActive(false);
                _keyImage.SetActive(false);
                _levelText.text = "";
                _completedText.text = "New Game";
            }
            else
            {
                this.gameObject.SetActive(true);
                _heartLayoutGroup.SetActive(true);
                _magicBar.SetActive(true);
                _goldImage.SetActive(true);
                _keyImage.SetActive(true);

                _levelText.text = game.gameScene.ToString();
                GlobalUtils.DestroyChildren(_heartLayoutGroup);
                for (int i = 0; i < game.heartPoints; i++)
                {
                    Instantiate(_heartImage, _heartLayoutGroup.transform);
                }

                float magicBarWidth = game.magicPoints * 17f;
                RectTransform magicRt = _magicBar.GetComponent<RectTransform>();
                magicRt.sizeDelta = new Vector2(magicBarWidth, magicRt.sizeDelta.y);

                _goldText.text = game.gold.ToString();
                _keyText.text = game.keys.ToString();


                _completedText.text = CalculateCompletion(game).ToString() + "%";
            }
        }

        private int CalculateCompletion(GameDetails game)
        {
            int completedTally = 0;
            completedTally += game.heartPoints - 3;
            completedTally += game.magicPoints - 3;
            completedTally *= 5;
            completedTally += game.starShards;
            completedTally += game.equipment.friends.Count;
            completedTally += game.equipment.hats.Count;
            completedTally += game.equipment.outfits.Count;
            completedTally += game.equipment.shoes.Count;
            completedTally += game.equipment.necklaces.Count;
            completedTally += game.equipment.rings.Count;


            return Mathf.RoundToInt(completedTally);
        }



    }
}
