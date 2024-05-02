using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PrincessAdventure
{
    public class PrincessHairColorSelector : MonoBehaviour
    {
        [SerializeField] private CustomizeCharGuiController _customCharGuiCtrl;
        [SerializeField] private GameObject _selectedCheck;
        [SerializeField] private PrincessHairColor _hairColor;

        public void UpdatePrincessHairColor()
        {
            _customCharGuiCtrl.UpdatePrincessHairColor(_hairColor);

            SetSelectedCheck(true);
        }

        public void SetSelectedCheck(bool isActive)
        {
            _selectedCheck.SetActive(isActive);
        }
    }
}
