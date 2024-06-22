using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace PrincessAdventure
{
    public class Interaction : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private InteractionTypes _interactionType;
        [SerializeField] private bool _companionCanDo;

        [Header("Optional")]
        [SerializeField] private Vector2 _direction;
        [SerializeField] private List<AffectedObjectController> _affectedObjects;

        [SerializeField] private GameObject _activeBefore;
        [SerializeField] private GameObject _activeAfter;

        private void Start()
        {
            if ((_affectedObjects == null || _affectedObjects.Count == 0) && this.GetComponent<AffectedObjectController>() != null)
            {
                _affectedObjects = new List<AffectedObjectController>();
                _affectedObjects.Add(this.GetComponent<AffectedObjectController>());
            }

            if(_interactionType == InteractionTypes.Lever)
            {
                LevelManager levelMgr = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<LevelManager>();

                levelMgr.AddToCallBackList(this.gameObject);
            }
        }

        public void LeverToggleCallback()
        {
            if (_interactionType == InteractionTypes.Lever)
            {
                if (_affectedObjects != null && _affectedObjects.Count > 0)
                    UpdateActiveInteractable(!_affectedObjects[0].IsToggled());
            }
        }

        public bool IsInteractionActive(bool isCompanion)
        {
            bool isActive = true;

            if(isCompanion &&_companionCanDo == false)
                return false;
            

            switch (_interactionType)
            {
                case InteractionTypes.Door:
                case InteractionTypes.MinorChest:
                case InteractionTypes.MajorChest:
                case InteractionTypes.Lever:
                    isActive = _affectedObjects[0].IsActive();
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
                    if (_affectedObjects[0].IsLocked())
                        word = "Unlock";
                    else
                        word = "Open";
                    break;
                case InteractionTypes.MinorChest:
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
                    
                    if (_activeAfter != null)
                        UpdateActiveInteractable(!_affectedObjects[0].IsToggled());

                    DoLeverPull();
                    break;
                case InteractionTypes.MajorChest:
                    if (_affectedObjects[0].IsLocked())
                        AttemptUnlock();
                    else
                        DoMajorTreasure();
                    break;
                case InteractionTypes.Message:
                case InteractionTypes.Talk:
                    DoMessage();
                    break;
                case InteractionTypes.Door:
                    if (_affectedObjects[0].IsLocked())
                        AttemptUnlock();
                    else
                        OpenDoor();
                    break;

            }

        }

        private void UpdateActiveInteractable(bool isToggled)
        {
            _activeAfter.SetActive(isToggled);
            _activeBefore.SetActive(!isToggled);
            
        }

        private void DoCliffJump()
        {
            CharacterController charCtrl = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>();
            charCtrl.AttemptCliffJump(_direction);
        }

        private void DoTreasureExplosion()
        {
            _affectedObjects[0].ToggleTheObject();
            TreasureExplosion treasure = _affectedObjects[0].gameObject.GetComponent<TreasureExplosion>();
            treasure.ThrowTreasure();
        }

        private void DoLeverPull()
        {
            foreach (AffectedObjectController affObj in _affectedObjects)
            {
                affObj.ToggleTheObject();
            }
        }

        private void DoMajorTreasure()
        {
            _affectedObjects[0].ToggleTheObject();
            MajorItemHandler itemHandler = _affectedObjects[0].gameObject.GetComponent<MajorItemHandler>();
            itemHandler.HandleTreasure();
        }

        private void AttemptUnlock()
        {
            if (GameManager.GameInstance.HasKey())
            {
                _affectedObjects[0].Unlock();
                GameManager.GameInstance.UseKey();
            }
            else
                _affectedObjects[0].FailedToUnlock();

        }

        private void DoMessage()
        {
            MessageInteractionController msgCtrl = this.gameObject.GetComponent<MessageInteractionController>();
            msgCtrl.ShowMessage();
        }

        private void OpenDoor()
        {
            foreach (AffectedObjectController affObj in _affectedObjects)
            {
                affObj.ToggleTheObject();
            }
        }

    }
}