using UnityEngine;
using System.Collections;


namespace PrincessAdventure
{
    public class Interaction : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private InteractionTypes _interactionType;
        [SerializeField] private int _interactionId;

        [Header("Optional")]
        [SerializeField] private Vector2 _direction;
        [SerializeField] private GameObject _activeBefore;
        [SerializeField] private GameObject _activeAfter;

        // Use this for initialization
        void Start()
        {
            //InteractionTypes 
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        public string GetInteractionWord()
        {
            string word = "!";

            switch (_interactionType)
            {
                case InteractionTypes.Door:
                case InteractionTypes.MinorChest:
                case InteractionTypes.MajorChest:
                    word = "Open";
                    break;
                case InteractionTypes.Talk:
                    word = "Talk";
                    break;
                case InteractionTypes.Jump:
                    word = "Jump";
                    break;
                default:
                    word = "!";
                    break;
            }

            return word;
        }

        public void DoInteraction(Vector2 directions)
        {
            switch(_interactionType)
            {
                case InteractionTypes.Jump:
                    DoCliffJump();
                    break;
                case InteractionTypes.MinorChest:
                    DoTreasureExplosion();
                    break;
            }

        }

        private void DoCliffJump()
        {
            CharacterController charCtrl = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>();
            charCtrl.AttemptCliffJump(_direction);
        }

        private void DoTreasureExplosion()
        {
            TreasureExplosion treasure = this.GetComponent<TreasureExplosion>();
            treasure.ThrowTreasure();

            StopInteractions();
        }

        private void StopInteractions()
        {
            BoxCollider2D col = this.GetComponent<BoxCollider2D>();
            col.enabled = false;

            _activeAfter.SetActive(true);
            _activeBefore.SetActive(false);

        }
    }
}