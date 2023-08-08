using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PrincessAdventure
{
    public class SelectorGuiController : MonoBehaviour
    {
        [SerializeField] private GameObject _highlightCursor;
        private RectTransform _highlightRectTransform;

        private void Start()
        {
            _highlightRectTransform = _highlightCursor.GetComponent<RectTransform>();
        }

        private void Update()
        {
            if(_highlightRectTransform != null)
                _highlightRectTransform.Rotate(new Vector3(0, 0, -1));

        }

        public void SetHighlightCursor(bool isActive)
        {
            _highlightCursor.SetActive(isActive);
        }
    }
}
