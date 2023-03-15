using UnityEngine;
using System.Collections;


namespace PrincessAdventure
{
    public class Interaction : MonoBehaviour
    {
        public InteractionTypes _interactionType;
        public int _interactionId;
        public Vector2 _direction;
        public string _tempText;

        // Use this for initialization
        void Start()
        {
            //InteractionTypes 
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void DoInteraction(Vector2 directions)
        {
            switch(_interactionType)
            {
                case InteractionTypes.Jump:
                    DoCliffJump();
                    break;
            }

        }

        private void DoCliffJump()
        {
            CharacterController charCtrl = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>();
            charCtrl.AttemptCliffJump(_direction);
        }
    }
}