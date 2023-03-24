using UnityEngine;
using System.Collections;

namespace PrincessAdventure
{

    public class GuiManager : MonoBehaviour
    {
        [SerializeField] GameplayGuiController _gameplayGui;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }


        public void LoadGui(ActiveGame gameDetails)
        {
            _gameplayGui.LoadGameplayGui(gameDetails);
        }

        public void EmptyOneHeart()
        {
            _gameplayGui.EmptyOneHeart();
        }

        public void HealOneHeart()
        {
            _gameplayGui.RefillOneHeart();
        }

        public void UpdateGoldText(int goldAmt)
        {
            _gameplayGui.UpdateGoldText(goldAmt);
        }

        public void UpdateKeyText(int keys)
        {
            _gameplayGui.UpdateKeyText(keys);

        }
    }
}
