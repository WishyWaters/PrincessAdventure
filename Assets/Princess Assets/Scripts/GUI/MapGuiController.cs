using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace PrincessAdventure
{
    public class MapGuiController : MonoBehaviour
    {
        [SerializeField] GameObject _startSelected;
        private GameObject _highlightedObject = null;

        // Start is called before the first frame update
        void Start()
        {
            InitializeMapPanel();
        }

        // Update is called once per frame
        void Update()
        {

            if (_highlightedObject != EventSystem.current.currentSelectedGameObject)
            {
                if (_highlightedObject != null && _highlightedObject.GetComponent<SelectorGuiController>() != null)
                    _highlightedObject.GetComponent<SelectorGuiController>().SetHighlightCursor(false);

                _highlightedObject = EventSystem.current.currentSelectedGameObject;

                if (_highlightedObject != null && _highlightedObject.GetComponent<SelectorGuiController>() != null)
                    _highlightedObject.GetComponent<SelectorGuiController>().SetHighlightCursor(true);

                SoundManager.SoundInstance.PlayUiNavigate();

                //TODO: Update Text based on what is highlighted
            }
        }

        public void InitializeMapPanel()
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(_startSelected);

        }
    }
}