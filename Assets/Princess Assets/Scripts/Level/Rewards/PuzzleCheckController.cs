using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PrincessAdventure
{
    public class PuzzleCheckController : MonoBehaviour
    {
        [SerializeField] private int _toggleSaveId;
        [SerializeField] private List<GameObject> _objectsToCheck;

        [SerializeField] private GameObject _objectToSetActive;
        [SerializeField] private GameObject _objectToAffect;
        [SerializeField] private AudioClip _fanfareClip;
        [SerializeField] private GameObject _fanfareEffect;

        [SerializeField] private AudioClip _failClip;


        private bool puzzleSolved;

        private void Start()
        {
            LevelManager levelMgr = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<LevelManager>();

            if (levelMgr.DoesToggleSaveExist(_toggleSaveId))
            {
                if(levelMgr.GetLevelToggle(_toggleSaveId))
                    HandlePuzzleCompletion(false);
            }

        }

        public void CheckPuzzle()
        {
            if (IsPuzzleCorrect() && !puzzleSolved)
                HandlePuzzleCompletion(true);
            else if(!puzzleSolved)
                SoundManager.SoundInstance.PlayEffectSound(_failClip);
        }

        private bool IsPuzzleCorrect()
        {
            foreach (GameObject go in _objectsToCheck)
            {
                if (go.CompareTag("OpenFire"))
                {
                    SmallFireController fireCtrl = go.GetComponent<SmallFireController>();

                    if (!fireCtrl.IsCorrect())
                        return false;
                }
            }

            return true;
        }

        private void HandlePuzzleCompletion(bool performFanfare)
        {
            puzzleSolved = true;

            if (_objectToSetActive != null)
                _objectToSetActive.SetActive(true);

            if(_objectToAffect)
            {
                AffectedObjectController affectCtrl = _objectToAffect.GetComponent<AffectedObjectController>();
                affectCtrl.ToggleTheObject();
            }

            UpdateSave(true);

            if (performFanfare)
                PlayFanFare();
        }

        private void PlayFanFare()
        {
            if (_fanfareClip != null)
                SoundManager.SoundInstance.PlayEffectSound(_fanfareClip);

            if (_fanfareEffect != null)
            {
                GameObject effect = Instantiate(_fanfareEffect, this.transform);

                ParticleSystem effectPSys = effect.GetComponent<ParticleSystem>();

                if (effect != null && effectPSys != null)
                    effectPSys.Play();
            }


        }

        private void UpdateSave(bool value)
        {
            //Call level manager and update object data
            LevelManager levelMgr = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<LevelManager>();

            levelMgr.SetLevelToggle(_toggleSaveId, value);
        }
    }
}
