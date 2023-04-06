using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace PrincessAdventure
{
    public class InteractController : MonoBehaviour
    {
        [SerializeField] TextMeshPro _textMesh;
        [SerializeField] bool _isCompanion;
        [SerializeField] List<AudioClip> _noticeClips;

        private BoxCollider2D _interactionTrigger;
        private Collider2D _currentCol;
        private Interaction _currentInteract;
        private Vector2 _lastDirection;


        private void Awake()
        {
            _interactionTrigger = this.GetComponent<BoxCollider2D>();
            //_textMesh.enabled = false;
            _textMesh.text = "";
        }


        public void UpdateOffset(Vector2 direction)
        {
            _lastDirection = direction;
       
            if (direction == Vector2.right)
            {
                _interactionTrigger.offset = new Vector2(0.8f, 0.3f);
                _interactionTrigger.size = new Vector2(0.7f, 1.1f);
            }
            else if (direction == Vector2.left)
            {
                _interactionTrigger.offset = new Vector2(-0.8f, 0.3f);
                _interactionTrigger.size = new Vector2(0.7f, 1.1f);
            }
            else if (direction == Vector2.up)
            {
                _interactionTrigger.offset = new Vector2(0f, 1f);
                _interactionTrigger.size = new Vector2(1f, 0.7f);
            }
            else if (direction == Vector2.down)
            {
                _interactionTrigger.offset = new Vector2(0f, -0.7f);
                _interactionTrigger.size = new Vector2(1f, 0.7f);
            }

        }

        public void UpdateCompanionOffset(Vector2 direction)
        {
            _lastDirection = direction;

            if (direction == Vector2.right)
            {
                _interactionTrigger.offset = new Vector2(0.5f, 0.1f);
                _interactionTrigger.size = new Vector2(0.5f, .6f);
            }
            else if (direction == Vector2.left)
            {
                _interactionTrigger.offset = new Vector2(-0.5f, 0.1f);
                _interactionTrigger.size = new Vector2(0.5f, .6f);
            }
            else if (direction == Vector2.up)
            {
                _interactionTrigger.offset = new Vector2(0f, .5f);
                _interactionTrigger.size = new Vector2(.6f, 0.5f);
            }
            else if (direction == Vector2.down)
            {
                _interactionTrigger.offset = new Vector2(0f, -0.5f);
                _interactionTrigger.size = new Vector2(.6f, 0.5f);
            }

        }

        public void AttemptInteraction()
        {
            if(_currentInteract != null)
            {
                if (_currentInteract.IsInteractionActive(_isCompanion))
                {
                    _currentInteract.DoInteraction(_lastDirection);
                    UpdateInteractable();
                }
                else
                {
                    _currentCol = null;
                    _currentInteract = null;
                    ToggleInteractText(false);
                }
            }
            else if(!_isCompanion)
            {
                int soundIndex = Random.Range(0, _noticeClips.Count);
                SoundManager.SoundInstance.PlayEffectSound(_noticeClips[soundIndex]);
                GameManager.GameInstance.PlayerIsNoticeable();
            }

        }

        private void UpdateInteractable()
        {
            if (_currentInteract != null)
            {
                if (_currentInteract.IsInteractionActive(_isCompanion))
                    ToggleInteractText(true);
                else
                {
                    _currentCol = null;
                    _currentInteract = null;
                    ToggleInteractText(false);
                }
            }
        }

        void OnTriggerEnter2D(Collider2D col)
        {
            if (_currentCol == null) {
                _currentCol = col;
                _currentInteract = col.GetComponent<Interaction>();
                UpdateInteractable();
            }

        }

        void OnTriggerExit2D(Collider2D col)
        {
            if (_currentCol == col)
            {
                _currentCol = null;
                _currentInteract = null;
                ToggleInteractText(false);
            }

        }

        private void ToggleInteractText(bool enabled)
        {
            //if (enabled == _textMesh.enabled)
            //    return;

            if (enabled)
            {
                //_textMesh.enabled = true;
                if(_textMesh.text != _currentInteract.GetInteractionWord())
                    _textMesh.text = _currentInteract.GetInteractionWord();
            }
            else
            {
                _textMesh.text = "";
                //_textMesh.enabled = false;
            }
        }

    }
}
