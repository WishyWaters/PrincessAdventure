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
        [SerializeField] private int _toggleSaveId;
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
            _isActive = true;

            //Talk to LevelManager & update active, locked, and toggled
            LevelManager levelMgr = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<LevelManager>();

            if(levelMgr.DoesToggleSaveExist(_toggleSaveId))
            {
                _isLocked = false;
                _isToggled = levelMgr.GetLevelToggle(_toggleSaveId);
                _activeAfter.SetActive(_isToggled);
                _activeBefore.SetActive(!_isToggled);

                if (_behavior == AffectedBehavior.OneTimeSave && _isToggled)
                    _isActive = false;
            }
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

            GameManager.GameInstance.UpdateCameraFollowToPlayer();
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
                    UpdateSave(_isToggled);
                    break;
                case AffectedBehavior.Toggle:
                    _isToggled = !_isToggled;
                    _activeAfter.SetActive(_isToggled);
                    _activeBefore.SetActive(!_isToggled);
                    UpdateSave(_isToggled);
                    break;
                case AffectedBehavior.TimedReset:
                    _isActive = false;
                    _isToggled = true;
                    _activeAfter.SetActive(true);
                    _activeBefore.SetActive(false);
                    StartCoroutine(TimerCountdown());
                    break;
            }
        }


        private IEnumerator TimerCountdown()
        {
            float timeRemaining = _timeToReset;
            //TODO: Initialize Timer UI
            int secondsRemaining = Mathf.FloorToInt(timeRemaining);

            GameManager.GameInstance.StartTimerGui(secondsRemaining);

            while (timeRemaining > 0)
            {
                secondsRemaining = Mathf.FloorToInt(timeRemaining);
                GameManager.GameInstance.UpdateTimerText(secondsRemaining);
                timeRemaining -= Time.deltaTime;

                yield return null;
            }

            //Close Timer UI
            GameManager.GameInstance.EndTimerGui();
            //Reset Toggle
            PlayToggleSound();
            _isActive = true;
            _isToggled = false;
            _activeAfter.SetActive(false);
            _activeBefore.SetActive(true);




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

        private void UpdateSave(bool value)
        {
            //Call level manager and update object data
            LevelManager levelMgr = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<LevelManager>();

            levelMgr.SetLevelToggle(_toggleSaveId, value);
        }

        public void Unlock()
        {
            SoundManager.SoundInstance.PlayClipAt(_onUnlock, this.transform.position);

            _isLocked = false;
            UpdateSave(false);
        }

        public void FailedToUnlock()
        {
            SoundManager.SoundInstance.PlayClipAt(_onNoKey, this.transform.position);

        }
    }
}