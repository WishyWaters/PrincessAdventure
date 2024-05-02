using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PrincessAdventure
{
    public class PrincessGlassesColorSelector : MonoBehaviour
    {
        [SerializeField] private CustomizeCharGuiController _customCharGuiCtrl;
        [SerializeField] private GameObject _selectedCheck;
        [SerializeField] private PrincessGlassesColor _glassesColor;

        public void UpdatePrincessGlassesColorColor()
        {
            _customCharGuiCtrl.UpdatePrincessGlassesColor(_glassesColor);

            SetSelectedCheck(true);
        }

        public void SetSelectedCheck(bool isActive)
        {
            _selectedCheck.SetActive(isActive);
        }
    }
}
