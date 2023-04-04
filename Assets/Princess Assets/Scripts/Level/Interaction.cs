using UnityEngine;
using System.Collections;


namespace PrincessAdventure
{
    public class Interaction : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private InteractionTypes _interactionType;

        [Header("Optional")]
        [SerializeField] private Vector2 _direction;
        [SerializeField] private AffectedObjectController _affectedObjectCtrl;

        [SerializeField] private GameObject _activeBefore;
        [SerializeField] private GameObject _activeAfter;


        private void Start()
        {
            //TODO: Check affected object, if toggled, update _activeBefore/_activeAfter

        }

        public bool IsInteractionActive()
        {
            bool isActive = true;

            switch (_interactionType)
            {
                case InteractionTypes.Door:
                case InteractionTypes.MinorChest:
                case InteractionTypes.MajorChest:
                case InteractionTypes.Lever:
                    isActive = _affectedObjectCtrl.IsActive();
                    break;
            }

            return isActive;
        }

        public string GetInteractionWord()
        {
            string word = "!";

            switch (_interactionType)
            {
                case InteractionTypes.Door:
                case InteractionTypes.MajorChest:
                case InteractionTypes.MinorChest:
                    //TODO:  Check if locked
                    word = "Open";
                    break;
                case InteractionTypes.Talk:
                    word = "Talk";
                    break;
                case InteractionTypes.Jump:
                    word = "Jump";
                    break;
                case InteractionTypes.Lever:
                    word = "Pull";
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
                case InteractionTypes.Lever:
                    DoLeverPull();
                    break;
                case InteractionTypes.MajorChest:
                    DoMajorTreasure();
                    break;
            }

            if (_activeAfter != null)
                UpdateActiveInteractable();

        }

        private void UpdateActiveInteractable()
        {
            if(_affectedObjectCtrl.IsToggled())
            {
                _activeAfter.SetActive(true);
                _activeBefore.SetActive(false);
            }
            else
            {
                _activeAfter.SetActive(false);
                _activeBefore.SetActive(true);
            }
        }

        private void DoCliffJump()
        {
            CharacterController charCtrl = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>();
            charCtrl.AttemptCliffJump(_direction);
        }

        private void DoTreasureExplosion()
        {
            _affectedObjectCtrl.ToggleTheObject();
            TreasureExplosion treasure = _affectedObjectCtrl.gameObject.GetComponent<TreasureExplosion>();
            treasure.ThrowTreasure();
        }

        private void DoLeverPull()
        {
            _affectedObjectCtrl.ToggleTheObject();
        }

        private void DoMajorTreasure()
        {
            _affectedObjectCtrl.ToggleTheObject();
            MajorItemHandler itemHandler = _affectedObjectCtrl.gameObject.GetComponent<MajorItemHandler>();
            itemHandler.HandleTreasure();
        }

    }
}