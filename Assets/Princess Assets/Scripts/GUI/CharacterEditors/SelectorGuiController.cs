using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PrincessAdventure
{
    public class SelectorGuiController : MonoBehaviour
    {
        [SerializeField] private GameObject _highlightCursor;

        public void SetHighlightCursor(bool isActive)
        {
            _highlightCursor.SetActive(isActive);
        }
    }
}
