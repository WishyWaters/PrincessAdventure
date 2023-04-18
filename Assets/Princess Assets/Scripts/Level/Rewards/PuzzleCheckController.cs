using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PrincessAdventure
{
    public class PuzzleCheckController : MonoBehaviour
    {
        [SerializeField] private int _id;
        [SerializeField] private List<GameObject> _objectsToCheck;

        [SerializeField] private GameObject _objectToSpawn;
        [SerializeField] private GameObject _objectToAffect;
        [SerializeField] private AudioClip _fanfareClip;
        [SerializeField] private GameObject _fanfareEffect;

        [SerializeField] private AudioClip _failClip;


        private bool puzzleSolved;

        private void Start()
        {
            //TODO:  Check scene manager for completed puzzle
            //If previously solved then
            //HandlePuzzleCompletion(false);
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

            if (_objectToSpawn != null)
                Instantiate(_objectToSpawn, this.transform.position, this.transform.rotation);

            if(_objectToAffect)
            {
                AffectedObjectController affectCtrl = _objectToAffect.GetComponent<AffectedObjectController>();
                affectCtrl.ToggleTheObject();
            }

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
    }
}
