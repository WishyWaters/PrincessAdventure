using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PrincessAdventure
{
    public class PrincessEyeColorSelector : MonoBehaviour
    {
        [SerializeField] private CustomizeCharGuiController _customCharGuiCtrl;
        [SerializeField] private GameObject _selectedCheck;
        [SerializeField] private PrincessEyeColor _eyeColor;

        public void UpdatePrincessEyeColor()
        {
            _customCharGuiCtrl.UpdatePrincessEyeColor(_eyeColor);

            SetSelectedCheck(true);
        }

        public void SetSelectedCheck(bool isActive)
        {
            _selectedCheck.SetActive(isActive);
        }
    }
}
