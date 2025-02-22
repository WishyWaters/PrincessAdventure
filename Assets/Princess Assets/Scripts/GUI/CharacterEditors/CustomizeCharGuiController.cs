using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace PrincessAdventure
{
    public class CustomizeCharGuiController : MonoBehaviour
    {
        [SerializeField] List<PrincessBodyColorSelector> bodyColorSelects;
        [SerializeField] List<PrincessHairStyleSelector> hairStyleSelects;
        [SerializeField] List<PrincessHairColorSelector> hairColorSelects;
        [SerializeField] List<PrincessEyeSelector> eyeShapeSelects;
        [SerializeField] List<PrincessEyeColorSelector> eyeColorSelects;
        [SerializeField] List<PrincessGlassesSelector> glassesSelects;
        [SerializeField] List<PrincessGlassesColorSelector> glassesColorSelects;

        private GameObject _highlightedObject = null;
        private GameScenes _returnScene = GameScenes.LevelLoadTester;
        private int _returnIndex = 0;

        private PrincessStyle _originalStyle = new PrincessStyle();

        // Start is called before the first frame update
        void Start()
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(bodyColorSelects[0].gameObject);


            LoadSelectedStyles(GameManager.GameInstance.GetPrincessStyle());
        }

        // Update is called once per frame
        void Update()
        {
            //Get currently selected object.
            //EventSystem.current.currentSelectedGameObject.
            //IF different than previouslySelectedGameObject then
            //  remove selection UI & update GO
            //  play cusor move sfx

            if(_highlightedObject != EventSystem.current.currentSelectedGameObject)
            {
                if(_highlightedObject != null && _highlightedObject.GetComponent<SelectorGuiController>() != null)
                    _highlightedObject.GetComponent<SelectorGuiController>().SetHighlightCursor(false);

                _highlightedObject = EventSystem.current.currentSelectedGameObject;

                if (_highlightedObject != null && _highlightedObject.GetComponent<SelectorGuiController>() != null)
                    _highlightedObject.GetComponent<SelectorGuiController>().SetHighlightCursor(true);

                //TODO: Play sfx
            }

        }

        public void InitializeCustomizerGui()
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(bodyColorSelects[0].gameObject);

            //if(firstCreation)
                //TODO: Display Create button
            //else
                //TODO: Display normal pause buttons
        }

        public void UpdatePrincessBodyColor(PrincessSkinColor newColor)
        {
            //Set all body color options as unselected
            foreach(PrincessBodyColorSelector colorSelector in bodyColorSelects)
            {
                colorSelector.SetSelectedCheck(false);
            }

            GameManager.GameInstance.SetBodyColor(newColor);

            //TODO: Play sfx
        }

        public void UpdatePrincessHairStyle(PrincessHairStyles hairStyle)
        {
            //unselect styles
            foreach (PrincessHairStyleSelector hairStyleSelector in hairStyleSelects)
            {
                hairStyleSelector.SetSelectedCheck(false);
            }

            GameManager.GameInstance.SetHairStyle(hairStyle);

            //TODO: Play sfx
        }

        public void UpdatePrincessHairColor(PrincessHairColor newColor)
        {
            //Set all body color options as unselected
            foreach (PrincessHairColorSelector colorSelector in hairColorSelects)
            {
                colorSelector.SetSelectedCheck(false);
            }

            GameManager.GameInstance.SetHairColor(newColor);

            //TODO: Play sfx
        }

        public void UpdatePrincessEyeShape(PrincessEyeShapes eye)
        {
            //unselect styles
            foreach (PrincessEyeSelector eyeSelector in eyeShapeSelects)
            {
                eyeSelector.SetSelectedCheck(false);
            }

            GameManager.GameInstance.SetEyeShape(eye);

            //TODO: Play sfx
        }

        public void UpdatePrincessEyeColor(PrincessEyeColor eyeColor)
        {
            //unselect styles
            foreach (PrincessEyeColorSelector eyeSelector in eyeColorSelects)
            {
                eyeSelector.SetSelectedCheck(false);
            }

            GameManager.GameInstance.SetEyeColor(eyeColor);

            //TODO: Play sfx
        }

        public void UpdatePrincessGlassesStyle(PrincessGlassesStyle glasses)
        {
            //unselect styles
            foreach (PrincessGlassesSelector glassSelector in glassesSelects)
            {
                glassSelector.SetSelectedCheck(false);
            }

            GameManager.GameInstance.SetGlassesStyle(glasses);

            //TODO: Play sfx
        }

        public void UpdatePrincessGlassesColor(PrincessGlassesColor glassesColor)
        {
            //unselect styles
            foreach (PrincessGlassesColorSelector glassColorSelector in glassesColorSelects)
            {
                glassColorSelector.SetSelectedCheck(false);
            }

            GameManager.GameInstance.SetGlassesColor(glassesColor);

            //TODO: Play sfx
        }

        private void LoadSelectedStyles(PrincessStyle currentStyle)
        {
            _originalStyle = currentStyle;

            bodyColorSelects[currentStyle.bodyColor].SetSelectedCheck(true);
            hairStyleSelects[currentStyle.hairStyle].SetSelectedCheck(true);
            hairColorSelects[currentStyle.hairColor].SetSelectedCheck(true);
            eyeShapeSelects[currentStyle.eyeStyle].SetSelectedCheck(true);
            eyeColorSelects[currentStyle.eyeColor].SetSelectedCheck(true);
            glassesSelects[currentStyle.glassesStyle].SetSelectedCheck(true);
            glassesColorSelects[currentStyle.glassesColor].SetSelectedCheck(true);

        }

        public void SaveCharacterAndResumeGame()
        {
            GameManager.GameInstance.ContinueGame();
        }
    }
}
