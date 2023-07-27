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

        public void UpdatePrincessEyeColor()
        {
            Color newColor = this.GetComponent<Image>().color;
            _customCharGuiCtrl.UpdatePrincessEyeColor(newColor);

            SetSelectedCheck(true);
        }

        public void SetSelectedCheck(bool isActive)
        {
            _selectedCheck.SetActive(isActive);
        }
    }
}
