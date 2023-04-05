using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PrincessAdventure
{
    public class AffectedObjectController : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private GameObject _activeBefore;
        [SerializeField] private GameObject _activeAfter;

        [SerializeField] private AudioClip _onActivation;
        [SerializeField] private AudioClip _onReset;

        [Header("Settings")]
        [SerializeField] private int _affectedObjectId;
        [SerializeField] private AffectedBehavior _behavior;
        [SerializeField] private float _timeToReset; 
        [SerializeField] private bool _cameraOnActivation;
        [SerializeField] private float _cameraMoveTime;
        [SerializeField] private bool _isLocked;
        [SerializeField] private AudioClip _onUnlock;
        [SerializeField] private AudioClip _onNoKey;

        private bool _isActive;
        private bool _isToggled;

        private void Start()
        {
            //TODO: Talk to SceneManager & update active, locked, and toggled
            _isActive = true;
        }

        public void ToggleTheObject()
        {
            if (_cameraOnActivation)
                StartCoroutine(MoveCamera());
            else
            {
                PlayToggleSound();
                DoToggle();
            }

        }

        private IEnumerator MoveCamera()
        {
            GameManager.GameInstance.UpdateCameraFollow(this.gameObject);

            float timeCount = 0;
            while (timeCount < _cameraMoveTime)
            {
                timeCount += Time.deltaTime;
                yield return null;
            }

            PlayToggleSound();
            DoToggle();

            timeCount = 0;
            while (timeCount < _cameraMoveTime)
            {
                timeCount += Time.deltaTime;
                yield return null;
            }

            GameManager.GameInstance.UpdateCameraFollow(GameObject.FindGameObjectWithTag("Player"));
        }

        private void PlayToggleSound()
        {
            if (_isToggled)
                SoundManager.SoundInstance.PlayClipAt(_onReset, this.transform.position);
            else
                SoundManager.SoundInstance.PlayClipAt(_onActivation, this.transform.position);

        }

        private void DoToggle()
        {
            switch (_behavior)
            {
                case AffectedBehavior.OneTimeResetOnLoad:
                    _activeAfter.SetActive(true);
                    _activeBefore.SetActive(false);
                    _isToggled = true;
                    _isActive = false;
                    break;
                case AffectedBehavior.OneTimeSave:
                    _activeAfter.SetActive(true);
                    _activeBefore.SetActive(false);
                    _isToggled = true;
                    _isActive = false;
                    UpdateSave();
                    break;
                case AffectedBehavior.Toggle:
                    _isToggled = !_isToggled;
                    _activeAfter.SetActive(_isToggled);
                    _activeBefore.SetActive(!_isToggled);
                    UpdateSave();
                    break;
            }
        }

        public bool IsActive()
        {
            return _isActive;
        }

        public bool IsToggled()
        {
            return _isToggled;
        }

        public bool IsLocked()
        {
            return _isLocked;
        }

        private void UpdateSave()
        {
            //TODO:  Call scene manager and update object data
        }

        public void Unlock()
        {
            SoundManager.SoundInstance.PlayClipAt(_onUnlock, this.transform.position);

            _isLocked = false;
            UpdateSave();
        }

        public void FailedToUnlock()
        {
            SoundManager.SoundInstance.PlayClipAt(_onNoKey, this.transform.position);

        }
    }
}