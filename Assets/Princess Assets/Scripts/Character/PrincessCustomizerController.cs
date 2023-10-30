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
        [SerializeField] private CustomizationCategory _hatCategory;
        [SerializeField] private CustomizationCategory _topCategory;
        [SerializeField] private CustomizationCategory _overlayCategory;
        [SerializeField] private CustomizationCategory _beltCategory;
        [SerializeField] private CustomizationCategory _pantsCategory;
        [SerializeField] private CustomizationCategory _handCategory;
        [SerializeField] private CustomizationCategory _shoulderCategory;


        [Header("References")]
        [SerializeField] private CustomizableCharacter _customizableCharacter;
        [SerializeField] private CharacterExpression _characterExpression;
        [SerializeField] private List<ExpressionData> _expressions = new List<ExpressionData>();

        [SerializeField] private List<CustomizationData> _hairStyles = new List<CustomizationData>();
        [SerializeField] private List<CustomizationData> _eyeShapes = new List<CustomizationData>();
        [SerializeField] private List<CustomizationData> _glasses = new List<CustomizationData>();

        [SerializeField] private List<HatEquip> _hats = new List<HatEquip>();
        [SerializeField] private List<OutfitEquip> _outfits = new List<OutfitEquip>();
        [SerializeField] private List<ShoeEquip> _shoes = new List<ShoeEquip>();

        [SerializeField] private CustomizationData _baldHatHair;
        [SerializeField] private CustomizationData _shortHatHair;
        [SerializeField] private CustomizationData _longHatHair;


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

        public void SetStyle(PrincessCustomizations customizations)
        {
            SetBodyColor(customizations.BodyColor);
            SetHairStyle(customizations.HairStyle);
            SetHairColor(customizations.HairColor);
            SetEyeShape(customizations.EyeStyle);
            SetEyeColor(customizations.EyeColor);
            SetGlassesShape(customizations.GlassesStyle);
            SetGlassesColor(customizations.GlassesColor);

        }

        public void SetEquipment(PrincessEquipment equipment, PrincessCustomizations customizations)
        {
            SetOutfit(equipment);
            SetHat(equipment, customizations);
            SetShoes(equipment);
        }

        private void SetHat(PrincessEquipment equipment, PrincessCustomizations customizations)
        {
            if(equipment.selectedHat == 1 || equipment.selectedHat == 9)
            {
                //remove hat
                if (_customizableCharacter.Customizer.HasCustomizationInCategory(_hatCategory))
                {
                    CustomizationData oldHatData = _customizableCharacter.Customizer.GetCustomizationDataInCategory(_hatCategory);
                    _customizableCharacter.Customizer.Remove(oldHatData);
                }
                //set hair to custom style
                SetHairStyle(customizations.HairStyle);
            }
            else
            {
                HatEquip hatData = _hats[equipment.selectedHat - 1];

                _customizableCharacter.Customizer.Add(hatData.hat);
                _customizableCharacter.Customizer.SetCustomizationMainColor(hatData.hat, hatData.color);
                if(hatData.hasDetail)
                {
                    _customizableCharacter.Customizer.SetCustomizationDetailIndex(hatData.hat, hatData.detailIndex);
                    _customizableCharacter.Customizer.SetCustomizationDetailColor(hatData.hat, hatData.detailColor);
                }

                if(hatData.requiresHatHair)
                {
                    CustomizationData hatHair = null;
                    CustomizationData oldHairData = _customizableCharacter.Customizer.GetCustomizationDataInCategory(_hairCategory);
                    Color hairColor = _customizableCharacter.Customizer.GetCustomizationMainColor(oldHairData);

                    switch(customizations.HairStyle)
                    {
                        case PrincessHairStyles.Mohawk:
                        case PrincessHairStyles.Bald:
                            hatHair = _baldHatHair;
                            break;
                        case PrincessHairStyles.Feathery:
                        case PrincessHairStyles.Spike:
                        case PrincessHairStyles.Floof:
                        case PrincessHairStyles.Pixie:
                            hatHair = _shortHatHair;
                            break;
                        default:
                            hatHair = _longHatHair;
                            break;
                    }
                    
                    _customizableCharacter.Customizer.Add(hatHair);
                    _customizableCharacter.Customizer.SetCustomizationMainColor(hatHair, hairColor);

                }
                else
                {
                    SetHairStyle(customizations.HairStyle);
                }
            }

        }

        private void SetOutfit(PrincessEquipment equipment)
        {
            //shoulders, hands, pants, belt, top, top overlay
            _customizableCharacter.Customizer.RemoveAllInCategory(_shoulderCategory);
            _customizableCharacter.Customizer.RemoveAllInCategory(_handCategory);
            _customizableCharacter.Customizer.RemoveAllInCategory(_pantsCategory);
            _customizableCharacter.Customizer.RemoveAllInCategory(_beltCategory);
            _customizableCharacter.Customizer.RemoveAllInCategory(_topCategory);
            _customizableCharacter.Customizer.RemoveAllInCategory(_overlayCategory);

            OutfitEquip outfit = _outfits[equipment.selectedOutfit - 1];

            if(outfit.top != null)
            {
                _customizableCharacter.Customizer.Add(outfit.top);
                _customizableCharacter.Customizer.SetCustomizationMainColor(outfit.top, outfit.topColor1);
                if (outfit.hasTopDetail)
                    _customizableCharacter.Customizer.SetCustomizationDetailIndex(outfit.top, outfit.topDetailIndex);
                _customizableCharacter.Customizer.SetCustomizationDetailColor(outfit.top, outfit.topColor2);

            }

            if (outfit.topOverlay != null)
            {
                _customizableCharacter.Customizer.Add(outfit.topOverlay);
                _customizableCharacter.Customizer.SetCustomizationMainColor(outfit.topOverlay, outfit.topOverColor1);
                _customizableCharacter.Customizer.SetCustomizationDetailColor(outfit.topOverlay, outfit.topOverColor2);
            }

            if(outfit.belt != null)
            {
                _customizableCharacter.Customizer.Add(outfit.belt);
                _customizableCharacter.Customizer.SetCustomizationMainColor(outfit.belt, outfit.beltColor1);
                if (outfit.hasBeltDetail)
                    _customizableCharacter.Customizer.SetCustomizationDetailIndex(outfit.belt, outfit.beltDetailIndex);
                _customizableCharacter.Customizer.SetCustomizationDetailColor(outfit.belt, outfit.beltColor2);
            }

            if (outfit.pants != null)
            {
                _customizableCharacter.Customizer.Add(outfit.pants);
                _customizableCharacter.Customizer.SetCustomizationMainColor(outfit.pants, outfit.pantColor1);
            }

            if (outfit.gloves != null)
            {
                _customizableCharacter.Customizer.Add(outfit.gloves);
                _customizableCharacter.Customizer.SetCustomizationMainColor(outfit.gloves, outfit.gloveColor1);
                _customizableCharacter.Customizer.SetCustomizationDetailColor(outfit.gloves, outfit.gloveColor2);
            }

            if (outfit.shoulder != null)
            {
                _customizableCharacter.Customizer.Add(outfit.shoulder);
                _customizableCharacter.Customizer.SetCustomizationMainColor(outfit.shoulder, outfit.shoulderColor1);
                _customizableCharacter.Customizer.SetCustomizationDetailColor(outfit.shoulder, outfit.shoulderColor2);
            }
        }

        private void SetShoes(PrincessEquipment equipment)
        {
            ShoeEquip shoeData = _shoes[equipment.selectedShoes-1];

            _customizableCharacter.Customizer.Add(shoeData.shoe);
            _customizableCharacter.Customizer.SetCustomizationMainColor(shoeData.shoe, shoeData.color);
            if(shoeData.hasDetail)
                _customizableCharacter.Customizer.SetCustomizationDetailIndex(shoeData.shoe, shoeData.detailIndex);
            _customizableCharacter.Customizer.SetCustomizationDetailColor(shoeData.shoe, shoeData.detailColor);


        }
    }
}