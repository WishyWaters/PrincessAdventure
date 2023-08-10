using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace PrincessAdventure
{
    public class QuestGuiController : MonoBehaviour
    {
        [SerializeField] GameObject _startSelected;
        private GameObject _highlightedObject = null;

        // Start is called before the first frame update
        void Start()
        {
            InitializeQuestPanel();
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

                //TODO: Play sfx
            }
        }

        public void InitializeQuestPanel()
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(_startSelected);

        }
    }
}