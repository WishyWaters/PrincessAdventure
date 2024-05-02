using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PrincessAdventure
{
    public class PrincessBodyColorSelector : MonoBehaviour
    {
        [SerializeField] private CustomizeCharGuiController _customCharGuiCtrl;
        [SerializeField] private GameObject _selectedCheck;
        [SerializeField] private PrincessSkinColor _skinColor;

        public void UpdatePrincessBodyColor()
        {
            _customCharGuiCtrl.UpdatePrincessBodyColor(_skinColor);

            SetSelectedCheck(true);
        }

        public void SetSelectedCheck(bool isActive)
        {
            _selectedCheck.SetActive(isActive);
        }
    }
}
