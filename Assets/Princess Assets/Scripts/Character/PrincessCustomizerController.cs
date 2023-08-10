using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomizableCharacters;

namespace PrincessAdventure
{
    public class PrincessCustomizerController : MonoBehaviour
    {
        [Header("Categories")]
        [SerializeField] private CustomizationCategory _hairCategory;
        [SerializeField] private CustomizationCategory _eyeCategory;
        [SerializeField] private CustomizationCategory _glassesCategory;
        [SerializeField] private CustomizationCategory _irisCategory;

        [Header("References")]
        [SerializeField] private CustomizableCharacter _customizableCharacter;
        [SerializeField] private CharacterExpression _characterExpression;
        [SerializeField] private List<ExpressionData> _expressions = new List<ExpressionData>();

        [SerializeField] private List<CustomizationData> _hairStyles = new List<CustomizationData>();
        [SerializeField] private List<CustomizationData> _eyeShapes = new List<CustomizationData>();
        [SerializeField] private List<CustomizationData> _glasses = new List<CustomizationData>();


        

        public void SetFace(FaceTypes faceType)
        {
            switch(faceType)
            {
                case FaceTypes.Default:
                    _characterExpression.SetToDefault();
                    break;
                case FaceTypes.Angry:
                    _characterExpression.SetExpression(_expressions[0]);
                    break;
                case FaceTypes.Smile:
                    _characterExpression.SetExpression(_expressions[1]);
                    break;
                case FaceTypes.Surprised:
                    _characterExpression.SetExpression(_expressions[2]);
                    break;
                case FaceTypes.Sleep:
                    _characterExpression.SetExpression(_expressions[3]);
                    break;
            }
                
        }

        public void SetBodyColor(Color newBodyColor)
        {
            _customizableCharacter.Customizer.SetBodyColor(newBodyColor);
        }

        public void SetHairStyle(PrincessHairStyles newHairStyle)
        {
            CustomizationData hairData = null;
            CustomizationData oldHairData = _customizableCharacter.Customizer.GetCustomizationDataInCategory(_hairCategory);
            Color hairColor = _customizableCharacter.Customizer.GetCustomizationMainColor(oldHairData);

            switch (newHairStyle)
            {
                case PrincessHairStyles.Bangs:
                    hairData = _hairStyles[0];
                    _customizableCharacter.Customizer.Add(hairData);
                    _customizableCharacter.Customizer.SetCustomizationMainColor(hairData, hairColor);
                    break;
                case PrincessHairStyles.Feathery:
                    hairData = _hairStyles[1];
                    _customizableCharacter.Customizer.Add(hairData);
                    _customizableCharacter.Customizer.SetCustomizationMainColor(hairData, hairColor);
                    break;
                case PrincessHairStyles.Down:
                    hairData = _hairStyles[2];
                    _customizableCharacter.Customizer.Add(hairData);
                    _customizableCharacter.Customizer.SetCustomizationMainColor(hairData, hairColor);
                    break;
                case PrincessHairStyles.Ponytail:
                    hairData = _hairStyles[3];
                    _customizableCharacter.Customizer.Add(hairData);
                    _customizableCharacter.Customizer.SetCustomizationMainColor(hairData, hairColor);
                    _customizableCharacter.Customizer.SetCustomizationDetailIndex(hairData, 0);
                    //_customizableCharacter.Customizer.SetCustomizationDetailColor(hairData, hairColor);
                    break;
                case PrincessHairStyles.DoubleBun:
                    hairData = _hairStyles[3];
                    _customizableCharacter.Customizer.Add(hairData);
                    _customizableCharacter.Customizer.SetCustomizationMainColor(hairData, hairColor);
                    _customizableCharacter.Customizer.SetCustomizationDetailIndex(hairData, 1);
                    //_customizableCharacter.Customizer.SetCustomizationDetailColor(hairData, hairColor);
                    break;
                case PrincessHairStyles.FaceFrame:
                    hairData = _hairStyles[4];
                    _customizableCharacter.Customizer.Add(hairData);
                    _customizableCharacter.Customizer.SetCustomizationMainColor(hairData, hairColor);
                    break;
                case PrincessHairStyles.TurnedOut:
                    hairData = _hairStyles[5];
                    _customizableCharacter.Customizer.Add(hairData);
                    _customizableCharacter.Customizer.SetCustomizationMainColor(hairData, hairColor);
                    break;
                case PrincessHairStyles.Mohawk:
                    hairData = _hairStyles[6];
                    _customizableCharacter.Customizer.Add(hairData);
                    _customizableCharacter.Customizer.SetCustomizationMainColor(hairData, hairColor);
                    break;
                case PrincessHairStyles.Spike:
                    hairData = _hairStyles[7];
                    _customizableCharacter.Customizer.Add(hairData);
                    _customizableCharacter.Customizer.SetCustomizationMainColor(hairData, hairColor);
                    break;
                case PrincessHairStyles.Floof:
                    hairData = _hairStyles[8];
                    _customizableCharacter.Customizer.Add(hairData);
                    _customizableCharacter.Customizer.SetCustomizationMainColor(hairData, hairColor);
                    break;
                case PrincessHairStyles.Slick:
                    hairData = _hairStyles[9];
                    _customizableCharacter.Customizer.Add(hairData);
                    _customizableCharacter.Customizer.SetCustomizationMainColor(hairData, hairColor);
                    _customizableCharacter.Customizer.SetCustomizationDetailIndex(hairData, 0);
                    break;
                case PrincessHairStyles.SlickPony:
                    hairData = _hairStyles[9];
                    _customizableCharacter.Customizer.Add(hairData);
                    _customizableCharacter.Customizer.SetCustomizationMainColor(hairData, hairColor);
                    _customizableCharacter.Customizer.SetCustomizationDetailIndex(hairData, 1);
                    break;
                case PrincessHairStyles.SlickBun:
                    hairData = _hairStyles[9];
                    _customizableCharacter.Customizer.Add(hairData);
                    _customizableCharacter.Customizer.SetCustomizationMainColor(hairData, hairColor);
                    _customizableCharacter.Customizer.SetCustomizationDetailIndex(hairData, 2);
                    break;
                case PrincessHairStyles.Pixie:
                    hairData = _hairStyles[10];
                    _customizableCharacter.Customizer.Add(hairData);
                    _customizableCharacter.Customizer.SetCustomizationMainColor(hairData, hairColor);
                    break;
                case PrincessHairStyles.Bald:
                    hairData = _hairStyles[11];
                    _customizableCharacter.Customizer.Add(hairData);
                    _customizableCharacter.Customizer.SetCustomizationMainColor(hairData, hairColor);
                    break;
                default:
                    hairData = _hairStyles[0];
                    _customizableCharacter.Customizer.Add(hairData);
                    _customizableCharacter.Customizer.SetCustomizationDetailColor(hairData, hairColor);
                    break;
            }

  
        }

        public void SetHairColor(Color newColor)
        {
            CustomizationData hairData = _customizableCharacter.Customizer.GetCustomizationDataInCategory(_hairCategory);

            _customizableCharacter.Customizer.SetCustomizationMainColor(hairData, newColor);
        }

        public void SetEyeShape(PrincessEyeShapes eyeShape)
        {
            CustomizationData eyeData = null;
            //CustomizationData oldEyeData = _customizableCharacter.Customizer.GetCustomizationDataInCategory(_eyeCategory);
            //Color eyeColor = _customizableCharacter.Customizer.GetCustomizationMainColor(oldEyeData);

            switch (eyeShape)
            {
                case PrincessEyeShapes.Big:
                    eyeData = _eyeShapes[0];
                    break;
                case PrincessEyeShapes.Middle:
                    eyeData = _eyeShapes[1];
                    break;
                default:
                    eyeData = _eyeShapes[2];
                    break;
            }

            _customizableCharacter.Customizer.Add(eyeData);
            //_customizableCharacter.Customizer.SetCustomizationMainColor(eyeData, eyeColor);

        }

        public void SetEyeColor(Color eyeColor)
        {
            CustomizationData irisData = _customizableCharacter.Customizer.GetCustomizationDataInCategory(_irisCategory);

            _customizableCharacter.Customizer.SetCustomizationMainColor(irisData, eyeColor);
        }

        public void SetGlassesShape(PrincessGlassesStyle style)
        {

            CustomizationData glassData = null;
            CustomizationData oldGlassData = null;
            Color glassColor = Color.white;

            if (_customizableCharacter.Customizer.HasCustomizationInCategory(_glassesCategory))
            {
                oldGlassData = _customizableCharacter.Customizer.GetCustomizationDataInCategory(_glassesCategory);
                glassColor = _customizableCharacter.Customizer.GetCustomizationMainColor(oldGlassData);
            }

            switch (style)
            {
                case PrincessGlassesStyle.None:
                    if (_customizableCharacter.Customizer.HasCustomizationInCategory(_glassesCategory))
                        _customizableCharacter.Customizer.Remove(oldGlassData);
                    break;
                case PrincessGlassesStyle.Round:
                    glassData = _glasses[0];
                    _customizableCharacter.Customizer.Add(glassData);
                    _customizableCharacter.Customizer.SetCustomizationMainColor(glassData, glassColor);
                    break;
                case PrincessGlassesStyle.Thick:
                    glassData = _glasses[1];
                    _customizableCharacter.Customizer.Add(glassData);
                    _customizableCharacter.Customizer.SetCustomizationMainColor(glassData, glassColor);
                    break;
                default:
                    glassData = _glasses[2];
                    _customizableCharacter.Customizer.Add(glassData);
                    _customizableCharacter.Customizer.SetCustomizationMainColor(glassData, glassColor);
                    break;
            }

        }

        public void SetGlassesColor(Color glassesColor)
        {
            CustomizationData glassData = _customizableCharacter.Customizer.GetCustomizationDataInCategory(_glassesCategory);

            if(glassData != null)
                _customizableCharacter.Customizer.SetCustomizationMainColor(glassData, glassesColor);
        }

        public void SetEquipment(PrincessEquipment equipment)
        {
            //TODO:  Set custom styles and colors by equipment

        }
    }
}