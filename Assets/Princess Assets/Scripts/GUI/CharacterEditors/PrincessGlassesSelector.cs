using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PrincessAdventure
{
    public class PrincessGlassesSelector : MonoBehaviour
    {
        [SerializeField] private CustomizeCharGuiController _customCharGuiCtrl;
        [SerializeField] private GameObject _selectedCheck;
        [SerializeField] private PrincessGlassesStyle _glassesStyle;

        public void UpdateGlassesStyle()
        {
            _customCharGuiCtrl.UpdatePrincessGlassesStyle(_glassesStyle);

            SetSelectedCheck(true);
        }

        public void SetSelectedCheck(bool isActive)
        {
            _selectedCheck.SetActive(isActive);
        }
    }
}
