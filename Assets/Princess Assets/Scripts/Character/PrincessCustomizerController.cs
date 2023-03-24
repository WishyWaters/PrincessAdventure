using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomizableCharacters;

namespace PrincessAdventure
{
    public class PrincessCustomizerController : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private CustomizableCharacter _customizableCharacter;
        [SerializeField] private CharacterExpression _characterExpression;
        [SerializeField] private List<ExpressionData> _expressions = new List<ExpressionData>();

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
    }
}